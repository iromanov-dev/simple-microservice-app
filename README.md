# simple-microservice-app

Users.API - сервис 1

Методы:

- AddUser - метод для добавления нового пользователя. Отправляет Event UserAddedEvent в очередь users-queue

Organizations.API - сервис 2

Методы:

- GetUsers - получение пейджинга пользователей по организации 
- GetAllUsers - получение списка всех пользователей
- GetAllOrganizations - получение списка всех организаций
- SetOrganization - прикрепление пользователя к организации

Реализована подписка на событие UserAddedEvent которая добавляет нового пользователя в БД.
Подключен Swagger для взаимодействия с API. 
