import { AuthenticateApi, HttpBearerAuth, LoginCredentials, WeatherForecastApi } from "../Weather.Api.Client.Sdk.Typescript/api";

let baseUrl = "http://localhost:5036";

let authenticateApi = new AuthenticateApi(baseUrl);

let loginCredentials = new LoginCredentials();
loginCredentials.username = "JonDoe";
loginCredentials.password = "JonDoe123$";

let authenticatePromise = authenticateApi.authenticate(loginCredentials);

authenticatePromise.then((token) => {
    let weatherForecastApi = new WeatherForecastApi(baseUrl);

    let bearer = new HttpBearerAuth();
    bearer.accessToken = token.body.value;

    weatherForecastApi.setDefaultAuthentication(bearer);
    let forecastsPromise = weatherForecastApi.getWeatherForecasts();

    forecastsPromise.then((forecasts) => {
        forecasts.body.forEach((forecast) => {
            console.log(`Id: ${forecast.id}`);
            console.log(`Date: ${forecast.date}`);
            console.log(`Summary: ${forecast.summary}`);
            console.log(`Temperature in celsius: ${forecast.temperatureC}`);
        });
    }, (error) => {
        console.log(error);
    });
}, (error) => {
    console.log(error);
});
