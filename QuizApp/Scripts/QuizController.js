var QuizController = (function (controller) {
    var usersList;
    var questionForm;
    var timer;
    var question;
    var answers;
    var correct;
    var challengeResults;
    var socket;

    var timerId;
    var lastQuestion;

    controller.init = function (_usersList, _questionForm, _uri, _challengeResult) {
        usersList = _usersList;
        questionForm = _questionForm;
        challengeResults = _challengeResult;
        timer = _questionForm.querySelector('#timer');
        question = _questionForm.querySelector('#question');
        answers = _questionForm.querySelectorAll('[id^="answer"]');
        correct = _questionForm.querySelector('#correct');
        socket = new WebSocket(_uri);
        socket.onopen = connectionOpened;
        socket.onclose = connectionClosed;
        socket.onmessage = messageReceived;
        socket.onerror = connectionError;
        hideControl(questionForm);
    };

    var connectionOpened = function (event) {
        alert('select a friend you want to challenge from the users list!');
    };

    var connectionClosed = function (event) {
        console.log('Connection closed!');
    };

    var connectionError = function (event) {
        alert("ERROR!\n" + event.data);
    };

    var messageReceived = function (event) {
        var data = JSON.parse(event.data);
        parseEvent(data);
    };

    var parseEvent = function (event) {
        if (event.Type) {
            switch (event.Type) {
                case "UserLogin":
                    userLoggedIn(event);
                    break;
                case "UserLogout":
                    userLoggedOut(event);
                    break;
                case "UserChallenge":
                    challengeReceived(event);
                    break;
                case "ChallengeAccepted":
                    challengeAccepted(event);
                    break;
                case "ChallengeRejected":
                    challengeRejected(event);
                    break;
                case "ChallengeResult":
                    challengeResult(event);
                    break;
                case "AnswerFeedback":
                    answerFeedback(event);
                    break;
                case "QuestionSend":
                    questionReceived(event);
                    break;
            };
        } else {
            console.log(event);
        }
    };

    var userLoggedIn = function (event) {
        var li = document.createElement('li');
        li.innerHTML = event.User.Username;
        li.id = event.User.Id;
        li.onclick = (function (user) {
            return function () {
                challengeUser(user);
            };        
        })(event.User);
        usersList.appendChild(li);
    };

    var userLoggedOut = function (event) {
        var li = usersList.querySelector('#' + event.User.Id);
        usersList.removeChild(li);
    };

    var challengeReceived = function (event) {
        var result = confirm(event.User.Username + ' challeged you! Do you accept the challenge?');
        if (result === true) {
            var challengeAccepted = {};
            challengeAccepted.Type = "ChallengeAccepted";
            challengeAccepted.User = event.User;
            socket.send(JSON.stringify(challengeAccepted));
            console.log('ChallengeAccepted');
            showControl(questionForm);
        }
        else {
            var challengeRejected = {};
            challengeRejected.Type = "ChallengeRejected";
            challengeRejected.User = event.User;
            socket.send(JSON.stringify(challengeRejected));
            console.log('ChallengeRejected');
            hideControl(questionForm);
        }
    };

    var challengeAccepted = function (event) {
        showControl(questionForm);
        alert(event.User.Username + ' accepted your challenge! Success!');
    };

    var challengeRejected = function (event) {
        alert(event.User.Username + ' rejected your challenge!');
    };

    var challengeResult = function (event) {
        hideControl(questionForm);
        challengeResults.innerHTML = event.Initiator.Username + ' score: ' + event.InitiatorScore + '<br/>' + event.Challenged.Username + ' score: ' + event.ChallengedScore;
    };

    var answerFeedback = function (event) {
        if (event.Correct) {
            //correct.innerHTML = "Correct!";
            console.log("correct!");
        }
        else {
            //correct.innerHTML = "Incorrect!";
            console.log("incorrect!");
        }
    };

    var questionReceived = function (event) {
        correct.innerHTML = "";
        var q = event.Question;
        lastQuestion = event.Question;
        question.innerHTML = q.Text;
        timer.innerHTML = q.SecondsToAnswer + ' sec. to answer!';
        timerId = setTimeout(timerTick, 1000, q.SecondsToAnswer);
        for (var i = 0; i < q.Answers.length; i++) {
            answers[i].innerHTML = q.Answers[i];
            answers[i].onclick = (function (index, q) {
                return function () {
                    if (timerId) {
                        clearTimeout(timerId);
                    }
                    var questionResponseEvent = {};
                    questionResponseEvent.Type = "QuestionResponse";
                    questionResponseEvent.AnswerIndex = index;
                    questionResponseEvent.Question = q;
                    socket.send(JSON.stringify(questionResponseEvent));
                };
            })(i, q);
        }
    };

    var timerTick = function (secondsToAnswer) {
        if (secondsToAnswer > 0) {
            timer.innerHTML = secondsToAnswer + ' sec. to answer!';
            timerId = setTimeout(timerTick, 1000, secondsToAnswer - 1);
        }
        else {
            var questionResponseEvent = {};
            questionResponseEvent.Type = "QuestionResponse";
            questionResponseEvent.AnswerIndex = -1;
            questionResponseEvent.Question = lastQuestion;
            socket.send(JSON.stringify(questionResponseEvent));
        }
    }

    var challengeUser = function (user) {
        var userChallenge = {};
        userChallenge.Type = "UserChallenge";
        userChallenge.User = user;
        socket.send(JSON.stringify(userChallenge));
        alert(user.Username + ' challenged!\n waiting for the response!');
    };   

    var hideControl = function (control) {
        control.style.display = 'none';
    };

    var showControl = function (control) {
        control.style.display = '';
    };

    return controller;
})(QuizController || {});