using Microsoft.EntityFrameworkCore;
using QuestionnaireApi.Models;

namespace QuestionnaireApi.Services
{
    public class QAContext : DbContext
    {
        public QAContext(DbContextOptions<QAContext> options) : base(options)
        { }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Topic> Topics { get; set; }
    }
}