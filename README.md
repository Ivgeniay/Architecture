# Readme

## Architecture

```
* Engine (Enterpoint)
    * ProjectInstaller
    * SceneInstallers
    * MonoArchBehaviour
```

Точка входа в приложение является класс Engine. Его необходимо поместить на любой пустой объект. 
![ENGINE](ReadmePic/Engine.png)

Класс Installer представлен в виде двух реализаций: ProjectInstaller и SceneInstaller. 

ProjectInstaller устанавливает все сервисы регистрируемые в нем на уровне всего приложения и загружается только один раз при первом запуске приложения. 

SceneInstaller - на уровне конкретной сцены соответственно. Загружает и выгружает ресурсы при переходе между сценами. 

Project и Scene Installer принимают в качестве ресурса для обработки реализацию класса ProjectSetting и SceneSetting соответственно, где и необходимо регистрировать сервисы.


Создадим класс Project_set который будет содержать в себе регистрацию зависимости некого сервиса GameController и его репозитория GameRepository и зарегистрируем его на уровне всего приложения в ProjectInstaller:
```
internal class Project_set : ProjectSetting
    {
        public override Dictionary<Type, Controller> BindControllers()
        {
            var controllers = new Dictionary<Type, Controller>();

            BindController<GameController>(controllers);

            return controllers;
        }

        public override Dictionary<Type, Repository> BindRepositories()
        {
            var repositories = new Dictionary<Type, Repository>();

            BindRepository<GameRepository>(repositories);

            return repositories;
        }
    }
```
После необходимо передать ProjectSetting в поле  Setting у экземпляра ProjectInstaller.

![ProjectSetting](ReadmePic/ProjectSetting.png)

Если все проделано правильно, то сервис GameController будет доступен по:
```
var controller = Engine.Instance.GetController<GameController>();
```

Подобную процедуру можно проделать и с SceneIntaller. Для доступа к экземпляру класса только на уровне одной сцены. 

---
## DI

---

## UIFramework

---

## Utilits