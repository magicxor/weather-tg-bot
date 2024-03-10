# weather-tg-bot

Inline weather bot

## Docker

```powershell
docker build . --progress=plain --file=WeatherTgBot/Dockerfile -t weather-tg-bot:latest
```

Environment variables: `WEATHERBOT_WeatherTgBot__TelegramBotApiKey`, `WEATHERBOT_WeatherTgBot__OpenMeteoApiUrl`

### API
https://open-meteo.com/
