module Secrets

open System.Threading
open Amazon
open Amazon.SecretsManager
open Amazon.SecretsManager.Model

let getSecret (secretName: string) =
    let cancellationSource = new CancellationTokenSource ()
    let client = new AmazonSecretsManagerClient (RegionEndpoint.EUWest2)
    let request = new GetSecretValueRequest ()
    request.SecretId <- secretName
    request.VersionStage <- "AWSCURRENT"
    async {
        let! (response: GetSecretValueResponse) = client.GetSecretValueAsync (request, cancellationSource.Token)
        return response.SecretString
    }
    
