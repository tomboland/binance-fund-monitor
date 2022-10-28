module BinanceBalances
open BinanceApi

let accountBalances (apiContext: BinanceApiContext) =
    accountInformationRequest apiContext
    |> function
    | Success accountInformation ->
        accountInformation.balances
    | Failure err -> failwith $"{err}"
