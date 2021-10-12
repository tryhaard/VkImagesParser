using VkGroupImageParser;
using VkGroupImageParser.Models;

Account account = new Account("username", "password"); // Логин и пароль от вк
VkBrowserWorker worker = new VkBrowserWorker(account, false); // второй аргумент отвечает за фоновый режим

worker.Auth(); // Авторизируемся
worker.DownloadImages("images","https://vk.com/fckbrain", 50); // папка для скачивания, линк на группу, кол-во картинок
worker.Close();