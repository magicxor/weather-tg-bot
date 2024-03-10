# weather-tg-bot

Inline weather bot

[demo](https://github.com/magicxor/weather-tg-bot/assets/8275793/0ed70c5d-1908-470e-a307-0b534466ba14)

## Docker

```powershell
docker build . --progress=plain --file=WeatherTgBot/Dockerfile -t weather-tg-bot:latest
```

Environment variables: `WEATHERBOT_WeatherTgBot__TelegramBotApiKey`, `WEATHERBOT_WeatherTgBot__OpenMeteoApiUrl`

### API
https://open-meteo.com/
