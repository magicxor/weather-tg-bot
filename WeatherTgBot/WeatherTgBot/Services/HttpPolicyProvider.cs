﻿using System.Net;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Wrap;

namespace WeatherTgBot.Services;

public static class HttpPolicyProvider
{
    public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(9);

    private const int MaxRetryAfterCount = 2;
    private static readonly TimeSpan DefaultRetryAfterTimeout = TimeSpan.FromSeconds(1);
    private static readonly IAsyncPolicy<HttpResponseMessage> RetryAfterPolicy = Policy
        .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.TooManyRequests && r.Headers.RetryAfter?.Delta != null)
        .WaitAndRetryAsync(
            MaxRetryAfterCount,
            (_, result, _) => result.Result.Headers.RetryAfter is { Delta: not null }
                ? result.Result.Headers.RetryAfter.Delta.Value
                : DefaultRetryAfterTimeout,
            (_, _, _, _) => Task.CompletedTask);

    private static readonly IEnumerable<TimeSpan> TelegramDelay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(0.3), retryCount: 3);
    private static readonly IAsyncPolicy<HttpResponseMessage> TelegramRetryPolicy = HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(TelegramDelay);
    public static readonly AsyncPolicyWrap<HttpResponseMessage> TelegramCombinedPolicy = Policy.WrapAsync(RetryAfterPolicy, TelegramRetryPolicy);

    private static readonly IEnumerable<TimeSpan> WeatherApiDelay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(0.3), retryCount: 3);
    private static readonly IAsyncPolicy<HttpResponseMessage> WeatherApiRetryPolicy = HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(WeatherApiDelay);
    public static readonly AsyncPolicyWrap<HttpResponseMessage> WeatherApiCombinedPolicy = Policy.WrapAsync(RetryAfterPolicy, WeatherApiRetryPolicy);
}
