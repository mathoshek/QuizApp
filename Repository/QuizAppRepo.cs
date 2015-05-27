using Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class QuizAppRepo
    {
        private ModelContainer ctx = new ModelContainer();

        //User Table
        public List<UserDto> getUsers()
        {
            List<UserDto> list = new List<UserDto>();
            foreach (var user in ctx.Users)
            {
                UserDto userDto = new UserDto();
                userDto.Username = user.Username;
                userDto.Password = user.Password;
                userDto.Type = user.Type;
                list.Add(userDto);
            }
            return list;
        }

        public UserDto addUser(string username, string password, string type)
        {
            User user = new User();
            user.Username = username;
            user.Password = password;
            user.Type = type;

            User created = ctx.Users.Add(user);
            ctx.SaveChanges();

            UserDto userDto = new UserDto();
            userDto.Username = created.Username;
            userDto.Password = created.Password;
            userDto.Type = created.Type;

            return userDto;
        }

        public UserDto getUser(string username)
        {
            User user = ctx.Users.FirstOrDefault(x => x.Username == username);
            if (user == null)
                return null;

            UserDto userDto = new UserDto();
            userDto.Username = user.Username;
            userDto.Password = user.Password;
            userDto.Type = user.Type;

            return userDto;
        }

        // QuizQuestion Table
        public List<QuizQuestionDto> getQuizQuestions()
        {
            List<QuizQuestionDto> list = new List<QuizQuestionDto>();
            foreach (var quizQuestion in ctx.QuizQuestions)
            {
                QuizQuestionDto quizQuestionDto = new QuizQuestionDto();
                quizQuestionDto.Id = quizQuestion.Id;
                quizQuestionDto.QuestionText = quizQuestion.QuestionText;
                quizQuestionDto.Answer1Text = quizQuestion.Answer1Text;
                quizQuestionDto.Answer1Correct = quizQuestion.Answer1Correct;
                quizQuestionDto.Answer2Text = quizQuestion.Answer2Text;
                quizQuestionDto.Answer2Correct = quizQuestion.Answer2Correct;
                quizQuestionDto.Answer3Text = quizQuestion.Answer3Text;
                quizQuestionDto.Answer3Correct = quizQuestion.Answer3Correct;
                quizQuestionDto.DomainId = quizQuestion.DomainId;
                quizQuestionDto.IsSingleChoice = quizQuestion.IsSingleChoice;

                list.Add(quizQuestionDto);
            }
            return list;
        }

        public QuizQuestionDto addQuizQuestion(string questionText, string answer1Text, bool answer1Correct,
            string answer2Text, bool answer2Correct, string answer3Text, bool answer3Correct, int domainId, bool isSingleChoice)
        {
            QuizQuestion quizQuestion = new QuizQuestion();
            quizQuestion.QuestionText = questionText;
            quizQuestion.Answer1Text = answer1Text;
            quizQuestion.Answer1Correct = answer1Correct;
            quizQuestion.Answer2Text = answer2Text;
            quizQuestion.Answer2Correct = answer2Correct;
            quizQuestion.Answer3Text = answer3Text;
            quizQuestion.Answer3Correct = answer3Correct;
            quizQuestion.DomainId = domainId;
            quizQuestion.IsSingleChoice = isSingleChoice;

            QuizQuestion created = ctx.QuizQuestions.Add(quizQuestion);
            ctx.SaveChanges();

            QuizQuestionDto quizQuestionDto = new QuizQuestionDto();
            quizQuestionDto.Id = created.Id;
            quizQuestionDto.QuestionText = created.QuestionText;
            quizQuestionDto.Answer1Text = created.Answer1Text;
            quizQuestionDto.Answer1Correct = created.Answer1Correct;
            quizQuestionDto.Answer2Text = created.Answer2Text;
            quizQuestionDto.Answer2Correct = created.Answer2Correct;
            quizQuestionDto.Answer3Text = created.Answer3Text;
            quizQuestionDto.Answer3Correct = created.Answer3Correct;
            quizQuestionDto.DomainId = created.DomainId;
            quizQuestionDto.IsSingleChoice = created.IsSingleChoice;

            return quizQuestionDto;
        }

        public QuizQuestionDto getQuizQuestion(int id)
        {
            QuizQuestion quizQuestion = ctx.QuizQuestions.FirstOrDefault(x => x.Id == id);
            if (quizQuestion == null)
                return null;

            QuizQuestionDto quizQuestionDto = new QuizQuestionDto();
            quizQuestionDto.Id = quizQuestion.Id;
            quizQuestionDto.QuestionText = quizQuestion.QuestionText;
            quizQuestionDto.Answer1Text = quizQuestion.Answer1Text;
            quizQuestionDto.Answer1Correct = quizQuestion.Answer1Correct;
            quizQuestionDto.Answer2Text = quizQuestion.Answer2Text;
            quizQuestionDto.Answer2Correct = quizQuestion.Answer2Correct;
            quizQuestionDto.Answer3Text = quizQuestion.Answer3Text;
            quizQuestionDto.Answer3Correct = quizQuestion.Answer3Correct;
            quizQuestionDto.DomainId = quizQuestion.DomainId;
            quizQuestionDto.IsSingleChoice = quizQuestion.IsSingleChoice;

            return quizQuestionDto;
        }

        // Quiz Table
        public List<QuizDto> getQuizes()
        {
            List<QuizDto> list = new List<QuizDto>();
            foreach (var quiz in ctx.Quizs)
            {
                QuizDto quizDto = new QuizDto();
                quizDto.Id = quiz.Id;
                quizDto.QuizTitle = quiz.QuizTitle;
                quizDto.Time = quiz.Time;
                quizDto.PassingScore = quiz.PassingScore;
                quizDto.NumQuestions = quiz.NumQuestions;
                quizDto.DomainId = quiz.DomainId;

                list.Add(quizDto);
            }
            return list;
        }

        public QuizDto addQuiz(string quizTitle, int time, float passingScore, int numQuestions, int domainId)
        {
            Quiz quiz = new Quiz();
            quiz.QuizTitle = quizTitle;
            quiz.Time = time;
            quiz.PassingScore = passingScore;
            quiz.NumQuestions = numQuestions;
            quiz.DomainId = domainId;

            Quiz creat = ctx.Quizs.Add(quiz);
            ctx.SaveChanges();

            QuizDto quizDto = new QuizDto();
            quizDto.Id = creat.Id;
            quizDto.QuizTitle = creat.QuizTitle;
            quizDto.Time = creat.Time;
            quizDto.PassingScore = creat.PassingScore;
            quizDto.NumQuestions = creat.NumQuestions;
            quizDto.DomainId = creat.DomainId;
            return quizDto;
        }

        public QuizDto getQuiz(int id)
        {
            Quiz quiz = ctx.Quizs.FirstOrDefault(x => x.Id == id);
            if (quiz == null)
                return null;

            QuizDto quizDto = new QuizDto();
            quizDto.Id = quiz.Id;
            quizDto.QuizTitle = quiz.QuizTitle;
            quizDto.Time = quiz.Time;
            quizDto.PassingScore = quiz.PassingScore;
            quizDto.NumQuestions = quiz.NumQuestions;
            quizDto.DomainId = quiz.DomainId;

            return quizDto;
        }
    }
}
