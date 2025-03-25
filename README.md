## Описание

Мой личный сайт, расположенный по адресу [mosalev.su](https://mosalev.su/).

## Процесс развёртывания

- Клонировать репозиторий на VPS сервер;
- Вместо конфигов .example создать свои и настроить;
- Заккоментировать в Proxy/nginx.conf часть с 443 портом, чтобы certbot смог получить ответ от сайта;
- Развернуть пакеты:

```docker
docker-compose up -d mos.proxy
docker-compose up -d certbot
```

- Раскомментировать в Proxy/nginx.conf часть с 443 портом;
- ???
- PROFIT

P.S. Для доступа к админке создать аккаунт своего пользователя в таблице Users.
