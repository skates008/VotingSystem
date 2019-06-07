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

    // BallotType
    [Table("BallotType", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.4.0")]
    public class BallotType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"ID", Order = 1, TypeName = "int")]
        [Index(@"PK_BallotType", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; } // ID (Primary key)

        [Column(@"ElectionID", Order = 2, TypeName = "int")]
        [Display(Name = "Election ID")]
        public int? ElectionId { get; set; } // ElectionID

        // Reverse navigation

        /// <summary>
        /// Child BallotTypeMappings where [BallotTypeMapping].[BallotTypeID] point to this entity (FK_BallotTypeMapping_BallotType)
        /// </summary>
        [JsonIgnore]
        public virtual System.Collections.Generic.List<BallotTypeMapping> BallotTypeMappings { get; set; } = new System.Collections.Generic.List<BallotTypeMapping>(); // BallotTypeMapping.FK_BallotTypeMapping_BallotType
        /// <summary>
        /// Child Voters where [Voters].[BallotType] point to this entity (FK_Voters_BallotType)
        /// </summary>
        [JsonIgnore]
        public virtual System.Collections.Generic.List<Voter> Voters { get; set; } = new System.Collections.Generic.List<Voter>(); // Voters.FK_Voters_BallotType

        // Foreign keys

        /// <summary>
        /// Parent Election pointed by [BallotType].([ElectionId]) (FK_BallotType_Elections)
        /// </summary>
        [JsonIgnore]
        [ForeignKey("ElectionId")] public virtual Election Election { get; set; } // FK_BallotType_Elections
    }

}
// </auto-generated>
