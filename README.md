

В проекте используется
            Coroutine - синглтон static класс для запуска корутин не из MonoBehaviour
            классов.
            Namespace: Routine;
            Запуск корутины: StartRoutine(IEnumerator ienumerator) => Coroutine;
            Остановка корутины: StopRoutine(Coroutine coroutine)


            ReviewVariable - generic класс с оповещение об изменении состояния;
            Observer - класс отслеживающий изменения всех ReviewVarible, которые
            в его конструктор были переданы (или через метод AddReview).


            MainQueue - класс реализующий выполнение методов переданных в него 
            по очереди с учетом приоритета. Методом AddTask(method, proirity);
            MQueue - фасад с синглтоном для MainQueue.
            Namespace: TaskQueues
            Добавление задачи: void AddTask (Action callback, TaskPriority priority = TaskPriority.Default)
            Завершение задачи: Action<object, string> onActionIsDoneEvent;
                    (invoke необходимо вызывать внутри самой задачи, где это необходимо.)
            Проверка состояния очереди: bool isBusy;
            Количество задач в очереди: int Count;

            Architecture:
            App - фасад над архитектурой.
