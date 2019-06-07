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

namespace VotingSiteAPI.Data.EfCfgMaps
{
    using Newtonsoft.Json;
    using VotingSiteAPI.Data;
    using VotingSiteAPI.Domain.Entities;

    // Contests
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.4.0")]
    public class ContestEfConfigMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Contest>
    {
        public ContestEfConfigMap()
            : this("dbo")
        {
        }

        public ContestEfConfigMap(string schema)
        {
            Property(x => x.BallotTypeId).IsOptional();
            Property(x => x.Title).IsOptional().IsUnicode(false);
            Property(x => x.Description).IsOptional().IsUnicode(false);
            Property(x => x.MaxVotes).IsOptional();
            Property(x => x.SortOrder).IsOptional();
            Property(x => x.ContestNameRecording).IsOptional().IsUnicode(false);
        }
    }

}
// </auto-generated>
