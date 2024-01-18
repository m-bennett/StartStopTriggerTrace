/* 
 * Keycloak Authentication API
 *
 * This API serves as a tool to retrieve an authentication response from Keycloak  ### References  - **Open API Specification** [https://swagger.io/specification/](https://swagger.io/specification/)  © Cimetrix 2020 
 *
 * OpenAPI spec version: 0.1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using RestSharp;

namespace StartStopTriggerTrace.Client
{
    /// <summary>
    /// A delegate to ExceptionFactory method
    /// </summary>
    /// <param name="methodName">Method name</param>
    /// <param name="response">Response</param>
    /// <returns>Exceptions</returns>
        public delegate Exception ExceptionFactory(string methodName, IRestResponse response);
}