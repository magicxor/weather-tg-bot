# weather-tg-bot

Inline Telegram weather bot

[Demo](https://github.com/magicxor/weather-tg-bot/assets/8275793/2910b994-d2ef-45b3-bfa8-d3e7df3b916e)

## Docker

```powershell
docker build . --progress=plain --file=WeatherTgBot/Dockerfile -t weather-tg-bot:latest
```

Environment variables: `WEATHERBOT_WeatherTgBot__TelegramBotApiKey`, `WEATHERBOT_WeatherTgBot__OpenMeteoApiUrl`

### API
https://open-meteo.com/
