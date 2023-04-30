# Wallets test

Тестовое здание.

## Задание

В дефолтном Blazor проекте создать страницу Wallets. На странице будет отображаться список счетов (Id, Address, Balance) в виде таблицы с сортировкой по балансу. 


### Условия:   

Использовать Netehereum.Web3 для общения с нодой 
Страница должна работать быстро 


### Бонус:  
Вынести общение с нодой в отдельное апи приложение.  


### Инструменты:  
* Библиотека Netehereum 
* ETH testnet Sepolia (рекомендуем к использованию https://www.alchemy.com/ и https://www.infura.io/ ) 
* База данных (прилагается файл wallets.sql) 


**Пояснение:  Балансы не хранятся в базе и их можно получить от ETH ноды. Предложите решение этой проблемы. **

## Startup

1. Клонировать репозиторий
2. `docker-compose up -d`
3. Создать в БД таблицу `Wallets` и заполнить её данными (`"ConnectionString": "User ID=dev;Password=dev;Server=localhost;Database=wallets-test;Pooling=true;MaxPoolSize=300"`)
4. Прописать Infura ApiKey в api/appsettings.json 
5. Запуск API (`dotnet run` в api)
6. Запуск сайта (`dotnet run` в site)
7. Открыть `http://localhost:5001` в браузере
