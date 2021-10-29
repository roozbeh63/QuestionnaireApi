using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Models
{
    [Table("_as_QA_QuestionOnTopic")]
    public class QuestionOnTopic : BaseModel
    {
        /// <summary>
        /// Topic to which the question belongs
        /// </summary>
        public long TopicId { get; set; }

        /// <summary>
        /// Question id
        /// </summary>
        public long QuestionId { get; set; }
    }
}