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

    // BallotTypeMapping
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.4.0")]
    public class BallotTypeMappingEfConfigMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<BallotTypeMapping>
    {
        public BallotTypeMappingEfConfigMap()
            : this("dbo")
        {
        }

        public BallotTypeMappingEfConfigMap(string schema)
        {
            Property(x => x.BallotTypeId).IsOptional();
            Property(x => x.ContestId).IsOptional();
            Property(x => x.SortOrder).IsOptional();

        }
    }

}
// </auto-generated>