# Gorzdrav Parser

Парсер для сбора информации о препаратах из раздела "Средства от диабета" с сайта Gorzdrav.org.

## Описание

Программа автоматически собирает данные о препаратах из каталога и сохраняет их в CSV-файл. Поддерживается парсинг для Москвы и Калининграда.

### Собираемые данные:
- ID товара
- Название препарата
- Рецептурность
- Производитель
- Активное вещество
- Цена
- Старая цена
- Ссылка на изображение
- Ссылка на товар
- Регион

### Требования
- Windows 10/11 или Linux
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Chrome браузер (версия 133+)

## Установка и запуск

#### Windows

```cmd
git clone https://github.com/VeraVinogradova/gorzdrav-parser
cd GorzdravParser/src/GorzdravParser
dotnet restore
dotnet run
```

#### Linux

```bash
git clone https://github.com/VeraVinogradova/gorzdrav-parser
cd GorzdravParser/src/GorzdravParser
dotnet restore
dotnet run
```

### Выберите регион:

1 - Москва
2 - Калининград

### Результаты (csv в UTF-8)

- result.csv - Москва
- result_kaliningrad.csv - Калининград

### Настройки

Файл src/GorzdravParser/appsettings.json:

```json
{
  "Human": {
    "MinDelayMs": 500,    // задержка между действиями (не рекомендуется менять, но можно тестить)
    "MaxDelayMs": 1000
  },
  "Browser": {
    "Headless": false     // true / false - можно убрать всплывающий браузер (true) и парсер будет работать на фоне
  }
}
```
