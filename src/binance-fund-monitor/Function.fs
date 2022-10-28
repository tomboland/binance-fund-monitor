namespace binance_fund_monitor

open Amazon.Lambda.Core
open Amazon.CDK.AWS
open Amazon.CDK.AWS.SecretsManager
open System
open BinanceApi
open BinanceBalances
open Secrets

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
()
type Payload = { input: string }

type Function() =
    member __.FunctionHandler (payload: Payload) (context: ILambdaContext) =
        let binanceApiKey = getSecret "binance-api-key" |> Async.RunSynchronously 
        let binanceApiSecret = getSecret "binance-api-secret" |> Async.RunSynchronously
        let apiContext = {
            ApiKey = BinanceApiKey binanceApiKey ;
            ApiSecret = BinanceApiSecret binanceApiSecret ;
            ApiRoot = BinanceApiRoot "https://api.binance.com" ;
        }
        accountBalances apiContext

