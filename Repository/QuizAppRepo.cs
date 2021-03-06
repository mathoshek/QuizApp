﻿using Repository.DTO;
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
                quizQuestionDto.SubdomainId = quizQuestion.SubdomainId;

                list.Add(quizQuestionDto);
            }
            return list;
        }

        public QuizQuestionDto addQuizQuestion(string questionText, string answer1Text, bool answer1Correct,
            string answer2Text, bool answer2Correct, string answer3Text, bool answer3Correct, int domainId, bool isSingleChoice, int subdomainId)
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
            quizQuestion.SubdomainId = subdomainId;

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
            quizQuestionDto.SubdomainId = created.SubdomainId;

            return quizQuestionDto;
        }

        public void UpdateQuizQuestion(int questionId, int domainId, string questionText, bool isSingleChoice,
            string answer1Text, bool answer1Correct,
            string answer2Text, bool answer2Correct,
            string answer3Text, bool answer3Correct, 
            int subdomainId)
        {
            QuizQuestion quizQuestion = ctx.QuizQuestions.FirstOrDefault(x => x.Id == questionId);
            quizQuestion.QuestionText = questionText;
            quizQuestion.Answer1Text = answer1Text;
            quizQuestion.Answer1Correct = answer1Correct;
            quizQuestion.Answer2Text = answer2Text;
            quizQuestion.Answer2Correct = answer2Correct;
            quizQuestion.Answer3Text = answer3Text;
            quizQuestion.Answer3Correct = answer3Correct;
            quizQuestion.DomainId = domainId;
            quizQuestion.IsSingleChoice = isSingleChoice;
            quizQuestion.SubdomainId = subdomainId;

            ctx.SaveChanges();
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
            quizQuestionDto.SubdomainId = quizQuestion.SubdomainId;

            return quizQuestionDto;
        }

        public void DeleteQuizQuestion(int questionId)
        {
            ctx.QuizQuestions.Remove(ctx.QuizQuestions.FirstOrDefault(x => x.Id == questionId));
            ctx.SaveChanges();
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

        //QuizQuestionDomain
        public List<QuizQuestionDomainDto> getQuestionDomains()
        {
            List<QuizQuestionDomainDto> list = new List<QuizQuestionDomainDto>();
            foreach (var questionDomain in ctx.QuizQuestionDomains)
            {
                QuizQuestionDomainDto questionDomainDto = new QuizQuestionDomainDto();
                questionDomainDto.Id = questionDomain.Id;
                questionDomainDto.Name = questionDomain.Name;
                list.Add(questionDomainDto);
            }
            return list;
        }

        public QuizQuestionDomainDto addQuestionDomain(string name)
        {
            QuizQuestionDomain questionDomain = new QuizQuestionDomain();
            questionDomain.Name = name;

            QuizQuestionDomain created = ctx.QuizQuestionDomains.Add(questionDomain);
            ctx.SaveChanges();

            QuizQuestionDomainDto questionDomainDto = new QuizQuestionDomainDto();
            questionDomainDto.Id = created.Id;
            questionDomainDto.Name = created.Name;

            return questionDomainDto;
        }

        public QuizQuestionDomainDto getQuestionDomain(int id)
        {
            QuizQuestionDomain questionDomain = ctx.QuizQuestionDomains.FirstOrDefault(x => x.Id == id);
            if (questionDomain == null)
                return null;

            QuizQuestionDomainDto questionDomainDto = new QuizQuestionDomainDto();
            questionDomainDto.Id = questionDomain.Id;
            questionDomainDto.Name = questionDomain.Name;

            return questionDomainDto;
        }

        // --------

        public QuizInstanceDto AddQuizInstanceToUser(string username, QuizInstanceDto quizInstanceDto)
        {
            QuizInstance qi = new QuizInstance();
            qi.IsStarted = quizInstanceDto.IsStarted;
            qi.QuizId = quizInstanceDto.QuizId;
            qi.StartDate = quizInstanceDto.StartTime;
            qi.IsFinished = quizInstanceDto.IsFinished;
            qi.FinishDate = quizInstanceDto.FinishDate;
            qi = ctx.QuizInstances.Add(qi);
            ctx.SaveChanges();

            User_QuizInstance uqi = new User_QuizInstance();
            uqi.QuizInstanceId = qi.Id;
            uqi.UserId = ctx.Users.FirstOrDefault(x => x.Username == username).Id;
            ctx.User_QuizInstance.Add(uqi);
            ctx.SaveChanges();

            QuizInstanceDto qidto = new QuizInstanceDto();
            qidto.Id = qi.Id;
            qidto.IsStarted = qi.IsStarted;
            qidto.QuizId = qi.QuizId;
            qidto.StartTime = qi.StartDate;
            qidto.IsFinished = qi.IsFinished;
            qidto.FinishDate = qi.FinishDate;
            return qidto;
        }

        public bool UserHasQuiz(string username, int quizId)
        {
            User u = ctx.Users.FirstOrDefault(x => x.Username == username);
            Quiz q = ctx.Quizs.FirstOrDefault(x => x.Id == quizId);

            return ctx.User_QuizInstance.FirstOrDefault(x => x.UserId == u.Id && ctx.QuizInstances.FirstOrDefault(y => y.Id == x.QuizInstanceId).QuizId == q.Id) != null;
        }

        public List<QuizInstanceDto> GetQuizInstancesForUser(string username)
        {
            User u = ctx.Users.FirstOrDefault(x => x.Username == username);
            List<QuizInstanceDto> list = new List<QuizInstanceDto>();

            foreach (var mapping in ctx.User_QuizInstance.Where(x => x.UserId == u.Id))
            {
                QuizInstance qi = ctx.QuizInstances.FirstOrDefault(x => x.Id == mapping.QuizInstanceId);
                QuizInstanceDto dto = new QuizInstanceDto();
                dto.Id = qi.Id;
                dto.IsStarted = qi.IsStarted;
                dto.QuizId = qi.QuizId;
                dto.StartTime = qi.StartDate;
                dto.IsFinished = qi.IsFinished;
                dto.FinishDate = qi.FinishDate;
                list.Add(dto);
            }
            return list;
        }

        public QuestionInstanceDto getQuestionInstance(int id)
        {
            QuizQuestionInstance qqi = ctx.QuizQuestionInstances.FirstOrDefault(x => x.Id == id);
            if (qqi == null)
                return null;

            QuestionInstanceDto qidto = new QuestionInstanceDto();
            qidto.Id = qqi.Id;
            qidto.QuestionId = qqi.QuizQuestionId;
            qidto.AnswerSaved = qqi.AnswerSaved;
            qidto.Choice1 = qqi.Choice1;
            qidto.Choice2 = qqi.Choice2;
            qidto.Choice3 = qqi.Choice3;

            return qidto;
        }

        public List<QuestionInstanceDto> getQuestionInstancesForQuizInstance(int id)
        {
            List<QuestionInstanceDto> list = new List<QuestionInstanceDto>();
            foreach (var qqi in ctx.QuizQuestionInstances.Where(x => x.QuizInstanceId == id))
            {
                QuestionInstanceDto qidto = new QuestionInstanceDto();
                qidto.Id = qqi.Id;
                qidto.QuestionId = qqi.QuizQuestionId;
                qidto.AnswerSaved = qqi.AnswerSaved;
                qidto.Choice1 = qqi.Choice1;
                qidto.Choice2 = qqi.Choice2;
                qidto.Choice3 = qqi.Choice3;
                list.Add(qidto);
            }
            return list;
        }

        public void addQuestionInstance(QuestionInstanceDto questionInstanceDto)
        {
            QuizQuestionInstance qqi = new QuizQuestionInstance();
            qqi.QuizQuestionId = questionInstanceDto.QuestionId;
            qqi.AnswerSaved = questionInstanceDto.AnswerSaved;
            qqi.QuizInstanceId = questionInstanceDto.QuizInstanceId;
            qqi.Choice1 = questionInstanceDto.Choice1;
            qqi.Choice2 = questionInstanceDto.Choice2;
            qqi.Choice3 = questionInstanceDto.Choice3;

            ctx.QuizQuestionInstances.Add(qqi);
            ctx.SaveChanges();
        }

        public List<QuizQuestionSubdomainDto> getQuizQuestionSubdomains()
        {
            List<QuizQuestionSubdomainDto> list = new List<QuizQuestionSubdomainDto>();
            if (ctx.QuizQuestionSubdomains == null)
                return list;
            foreach (var quizQuestionSubdomain in ctx.QuizQuestionSubdomains)
            {
                QuizQuestionSubdomainDto dto = new QuizQuestionSubdomainDto();
                dto.Id = quizQuestionSubdomain.Id;
                dto.Name = quizQuestionSubdomain.Name;
                dto.DomainId = quizQuestionSubdomain.QuizQuestionDomainId;
                list.Add(dto);
            }

            return list;
        }

        public QuizQuestionSubdomainDto addQuizQuestionSubdomain(string name, int domainId)
        {
            QuizQuestionSubdomain qqs = new QuizQuestionSubdomain();
            qqs.Name = name;
            qqs.QuizQuestionDomainId = domainId;
            qqs = ctx.QuizQuestionSubdomains.Add(qqs);
            ctx.SaveChanges();

            QuizQuestionSubdomainDto qqsdto = new QuizQuestionSubdomainDto();
            qqsdto.Id = qqs.Id;
            qqsdto.Name = qqs.Name;
            qqsdto.DomainId = qqs.QuizQuestionDomainId;
            return qqsdto;
        }

        public QuizInstanceDto GetQuizInstance(int quizInstanceId)
        {
            QuizInstance qi = ctx.QuizInstances.FirstOrDefault(x => x.Id == quizInstanceId);
            if (qi == null)
                return null;
            QuizInstanceDto qidto = new QuizInstanceDto();
            qidto.Id = qi.Id;
            qidto.IsStarted = qi.IsStarted;
            qidto.QuizId = qi.QuizId;
            qidto.StartTime = qi.StartDate;
            qidto.IsFinished = qi.IsFinished;
            qidto.FinishDate = qi.FinishDate;

            return qidto;
        }

        public void StartQuizInstance(int quizInstanceId)
        {
            QuizInstance qi = ctx.QuizInstances.FirstOrDefault(x => x.Id == quizInstanceId);
            if (qi == null)
                return;
            qi.IsStarted = true;
            qi.StartDate = DateTime.Now;
            ctx.SaveChanges();
        }

        public void FinishQuizInstance(int quizInstanceId)
        {
            QuizInstance qi = ctx.QuizInstances.FirstOrDefault(x => x.Id == quizInstanceId);
            if (qi == null)
                return;

            qi.IsFinished = true;
            qi.FinishDate = DateTime.Now;
            ctx.SaveChanges();
        }

        public void SaveQuestionInstanceAnswer(int questionInstanceId, bool first, bool second, bool third)
        {
            QuizQuestionInstance qqi = ctx.QuizQuestionInstances.FirstOrDefault(x => x.Id == questionInstanceId);
            qqi.AnswerSaved = true;
            qqi.Choice1 = first;
            qqi.Choice2 = second;
            qqi.Choice3 = third;

            ctx.SaveChanges();
        }
    }
}
