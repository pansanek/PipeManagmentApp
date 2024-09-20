# Pipe Management Application

## Описание

Это веб приложение для управления трубами и пакетами труб. Оно позволяет добавлять, редактировать и удалять трубы и пакеты, а также управлять их связями.

## Установка

Для корректной работы приложения необходимо настроить подключение к базе данных PostgreSQL. Выполните следующий шаг:

### Настройка `appsettings.json`

В файле `appsettings.json`, который находится в корне проекта, необходимо указать параметры подключения к вашей базе данных. Найдите раздел `"ConnectionStrings"` и обновите его следующим образом:

```json
"ConnectionStrings": {
  "WebApiDatabase": "Host=localhost; Database=PipeManagement; Username=YourUsername; Password=YourPassword"
}
```

- **Host**: Адрес сервера базы данных. По умолчанию используется `localhost`, что подразумевает, что база данных работает на том же сервере, что и приложение.
- **Database**: Название вашей базы данных. По умолчанию используется `PipeManagement`.
- **Username**: Ваше имя пользователя для подключения к базе данных.
- **Password**: Ваш пароль для подключения к базе данных.

В данном примере замените `YourUsername` и `YourPassword`.

Далее откройте окно `Package Manager Console` и введите следующую команду:
```bash
Update-Database
```

В pgAdmin должна появиться созданная БД с таблицами заполненными тестовыми данными.
## Работа приложения
#### Главная страница
<img src="readme_images/home_page.png" width="700">

#### Список труб
<img src="readme_images/pipe_list_page.png" width="700">

#### Окно фильтра для списка труб
<img src="readme_images/pipe_list_filter_window.png" width="700">

#### Примененный фильтр на странице списка труб
<img src="readme_images/pipe_list_filter_applied_page.png" width="700">

#### Страница добавления трубы
<img src="readme_images/pipe_add_page.png" width="700">

#### Страница удаления трубы
<img src="readme_images/pipe_delete_page.png" width="700">

#### Страница редактирования трубы
<img src="readme_images/pipe_edit_page.png" width="700">

#### Список пакетов
<img src="readme_images/bundle_list_page.png" width="700">

#### Окно фильтра для списка пакетов
<img src="readme_images/bundle_list_filter_window.png" width="700">

#### Страница добавления пакета
<img src="readme_images/bundle_add_page.png" width="700">

#### Страница удаления пакета
<img src="readme_images/bundle_delete_page.png" width="700">

#### Страница редактирования пакета
<img src="readme_images/bundle_edit_page.png" width="700">
