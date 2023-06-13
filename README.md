# Quiz
Клиент-Серверная викторина на двух игроков с использованием SignalR и WPF.
## Задача
Задачей было написать викторину для двух игроков, взаимодействие которых происходит через сервер. Четких требований обозначено не было, поэтому для выполнения этого задания я выбрал `WPF`
для клиентской части и `SignalR` для серверной.
# Сервер
## Вопросы
База вопросов была приложена к заданию, заполнение базы данных вопросами происходит в [сиде контекста базы данных](https://github.com/SMarkls/Quiz/blob/master/Server/ApplicationDbContextSeed.cs).
## Сущности
Сущности сервера, обеспечивающие игру:
* Игровая сессия.  ([GameSession.cs](https://github.com/SMarkls/Quiz/blob/master/Server/Models/GameSession.cs))
* Состояние игры. ([GameState.cs](https://github.com/SMarkls/Quiz/blob/master/Server/Models/GameState.cs))
* Вопрос. ([Question.cs](https://github.com/SMarkls/Quiz/blob/master/Server/Models/Question.cs))
* Пользователь. ([User.cs](https://github.com/SMarkls/Quiz/blob/master/Server/Models/User.cs))
* Модель представления сессии. ([SessionViewModel.cs](https://github.com/SMarkls/Quiz/blob/master/Server/Models/SessionViewModel.cs))
## 
``` mermaid
---
title: Сущность GameSession
---
classDiagram
class GameSession {
	long Id
	string Name
	List~Player~ Players
	GameState State
  List~Question~ Questions
}
```


``` mermaid
---
title: Сущность GameState
---
classDiagram
class GameState {
 long Id
 int step
 int CorrectAnswers1Player
 int COrrectAnswers2Player
 Question CurrentQuestion
}
```

``` mermaid
---
title: Сущность Question
---
classDiagram
class Question {
 long Id
 string Text
 string Answer1
 string Answer2
 string Answer3
 string Answer4
 int CorrectAnswer
 List~GameSession~ Sessions
}
```

``` mermaid
---
title: Сущность User
---
classDiagram
class User {
 long Id
 string ConnectionId
 string Name
 GameSession GameSession
}
```

## Диаграмма классов
``` mermaid
classDiagram
	direction RL
	GameState "1" <-- "1" GameSession
	User "1..2" <-- "1" GameSession
	Question "1..20" <-- "1" GameSession
  Question "1" <-- "1" GameState
```

## Хаб
Логика серверной части игры написана в классе Хаба ([GameHub.cs](https://github.com/SMarkls/Quiz/blob/master/Server/Hubs/GameHub.cs)), который работает с событиями. Есть два типа событий: **клиентские** и **серверные**.
Клиентские события носят названия публичных методов Хаба:
* MakeStep. Событие сделанного хода на стороне клиента.
* Connect. Событие попытки присоединения пользователя к комнате.
* ClientLost. Событие отсоединения клиента от игры до её окончания.
Серверные события носят названия методов класса главного окна клиента:
* Message. Сообщение со стороны сервера
* EndGame. Событие об окончании игры.
* StepMaked. Событие сделанного хода одним из двух игроков.

# Клиент
Клиентская часть создана на WPF. Представляет собой одну форму, на которой в зависимости от состояния появляются два компонента:
* Страница присоединения к игре. ([ConnectionPage](https://github.com/SMarkls/Quiz/tree/master/Client/Pages))  
![image](https://github.com/SMarkls/Quiz/assets/91720469/43f66fb0-a808-40b7-9ccf-09bba6d7b469)
* Карточка викторины с вопросом. ([QuizCard](https://github.com/SMarkls/Quiz/tree/master/Client/Components/QuizCard))  
![image](https://github.com/SMarkls/Quiz/assets/91720469/18b9fb5f-2e8e-4c83-9aa9-843d5eb374c5)
# 
Игра длится до тех пор пока не закончатся все вопросы. По окончании игры на клиент отправляется событие о том, что игра окончена и нужно подводить итоги.

