# Cars

## Environment variables
```
>> export CONNECTION_STRING = 'Host=localhost;Port=3306;Database=carsdb;Uid=root;Pwd=password;SslMode=None'
```

```
>> curl --request POST 'http://localhost:5276/api/cars' --header 'Content-Type: application/json' --data '{\"name\": \"bmw\", \"available\" : \"true\"}'

{"id":"1",name":"bmw","available":false}
```

# Cars.Tests

## Environment variables
```
>> export CONNECTION_STRING = 'Host=localhost;Port=3306;Database=carsdb;Uid=root;Pwd=password;SslMode=None'

>> export CONNECTION_STRING = 'Host=localhost;Port=3306;Database=carsdb;Uid=root;Pwd=password;SslMode=None'
```

```
>> dotnet test

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: < 1 ms - Cars.IntegrationTests.dll (net6.0)
```

# Run integration tests using Docker
```
>> docker-compose -f docker-compose-integration.yml up --build --abort-on-container-exit
```