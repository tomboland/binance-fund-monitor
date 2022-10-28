### Lambda Tooling
dotnet new -i Amazon.Lambda.Templates
dotnet tool install -g Amazon.Lambda.Tools

### Create project
dotnet new lambda.EmptyFunction -lang f#
dotnet new sln
dotnet sln add **/**/*.fsproj


### IAM
aws iam create-role --role-name my-lambda-role --assume-role-policy-document file:///dev/stdin < binance-functions-role.json
aws iam put-role-policy --role-name binance-functions-role --policy-name secretsmanager --policy-document file:///dev/stdin < binance-functions-role-secretsmanager-policy.json


### Deploy
cd src/binance-fund-monitor
dotnet lambda deploy-function binance-function
dotnet lambda invoke-function binance-function --payload '{"input": "hi"}'
