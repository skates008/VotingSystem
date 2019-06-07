// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.7
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSiteAPI.Domain.Entities
{
    using Newtonsoft.Json;

    // Contests
    [Table("Contests", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.4.0")]
    public class Contest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"ID", Order = 1, TypeName = "int")]
        [Index(@"PK_Contests", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; } // ID (Primary key)

        [Column(@"BallotTypeID", Order = 2, TypeName = "int")]
        [Display(Name = "Ballot type ID")]
        public int? BallotTypeId { get; set; } // BallotTypeID

        [Column(@"Title", Order = 3, TypeName = "varchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; } // Title (length: 50)

        [Column(@"Description", Order = 4, TypeName = "varchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Description")]
        public string Description { get; set; } // Description (length: 50)

        [Column(@"MaxVotes", Order = 5, TypeName = "int")]
        [Display(Name = "Max votes")]
        public int? MaxVotes { get; set; } // MaxVotes

        [Column(@"SortOrder", Order = 6, TypeName = "int")]
        [Display(Name = "Sort order")]
        public int? SortOrder { get; set; } // SortOrder

        [Column(@"ContestNameRecording", Order = 7, TypeName = "varchar")]
        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Contest name recording")]
        public string ContestNameRecording { get; set; } // ContestNameRecording (length: 255)

        // Reverse navigation

        /// <summary>
        /// Child BallotTypeMappings where [BallotTypeMapping].[ContestID] point to this entity (FK_BallotTypeMapping_Contests)
        /// </summary>
        [JsonIgnore]
        public virtual System.Collections.Generic.List<BallotTypeMapping> BallotTypeMappings { get; set; } = new System.Collections.Generic.List<BallotTypeMapping>(); // BallotTypeMapping.FK_BallotTypeMapping_Contests
        /// <summary>
        /// Child Candidates where [Candidates].[ContestID] point to this entity (FK_Candidates_Contests)
        /// </summary>
        [JsonIgnore]
        public virtual System.Collections.Generic.List<Candidate> Candidates { get; set; } = new System.Collections.Generic.List<Candidate>(); // Candidates.FK_Candidates_Contests
    }

}
// </auto-generated>
