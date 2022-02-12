/*
 * API
 *
 * An Api to perform weather forecasts
 *
 * The version of the OpenAPI document: 1
 * Contact: jon.doe@gmail.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using Xunit;

using Weather.Api.Client.Sdk.DotNet.Client;
using Weather.Api.Client.Sdk.DotNet.Api;
// uncomment below to import models
//using Weather.Api.Client.Sdk.DotNet.Model;

namespace Weather.Api.Client.Sdk.DotNet.Test.Api
{
    /// <summary>
    ///  Class for testing WeatherForecastApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class WeatherForecastApiTests : IDisposable
    {
        private WeatherForecastApi instance;

        public WeatherForecastApiTests()
        {
            instance = new WeatherForecastApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of WeatherForecastApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' WeatherForecastApi
            //Assert.IsType<WeatherForecastApi>(instance);
        }

        /// <summary>
        /// Test DeleteWeatherForecast
        /// </summary>
        [Fact]
        public void DeleteWeatherForecastTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //long id = null;
            //instance.DeleteWeatherForecast(id);
        }

        /// <summary>
        /// Test GetWeatherForecast
        /// </summary>
        [Fact]
        public void GetWeatherForecastTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //long id = null;
            //var response = instance.GetWeatherForecast(id);
            //Assert.IsType<WeatherForecastDto>(response);
        }

        /// <summary>
        /// Test GetWeatherForecasts
        /// </summary>
        [Fact]
        public void GetWeatherForecastsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.GetWeatherForecasts();
            //Assert.IsType<List<WeatherForecastDto>>(response);
        }

        /// <summary>
        /// Test PostWeatherForecast
        /// </summary>
        [Fact]
        public void PostWeatherForecastTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //WeatherForecastDto weatherForecastDto = null;
            //var response = instance.PostWeatherForecast(weatherForecastDto);
            //Assert.IsType<WeatherForecastDto>(response);
        }

        /// <summary>
        /// Test PutWeatherForecast
        /// </summary>
        [Fact]
        public void PutWeatherForecastTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //long id = null;
            //WeatherForecastDto weatherForecastDto = null;
            //instance.PutWeatherForecast(id, weatherForecastDto);
        }
    }
}
