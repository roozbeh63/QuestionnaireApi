using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Models
{
    [Table("_as_QA_Answer")]
    public class Answer : BaseModel
    {
        /// <summary>
        /// Question to which this answer is given
        /// </summary>
        public long QuestionId { get; set; }

        /// <summary>
        /// Questionnaire
        /// </summary>
        public long QuestionnaireId { get; set; }

        /// <summary>
        /// Submission date of the answer
        /// </summary>
        public DateTime SubmissionDate { get; set; }

        /// <summary>
        /// The answer would be parsed in XML
        /// </summary>
        public string GivenAnswer { get; set; }
    }
}