using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AShebetTreeNode.Server.Migrations
{
    /// <inheritdoc />
    public partial class UserNodeRecursiveRelations_CTE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            System.Text.StringBuilder query = new();
            query.Append("CREATE OR REPLACE VIEW public.\"UserNodeRelations\" ")
                 .Append("AS WITH RECURSIVE cte (\"a\", \"b\") AS ( ")
                 .Append("SELECT \"Id\", \"Id\" FROM public.\"UserNodes\" ")
                 .Append("UNION SELECT e.\"Id\", cte.\"b\" FROM cte ")
                 .Append("INNER JOIN public.\"UserNodes\" e ON e.\"ParentId\" = cte.\"a\") ")
                 .Append("SELECT \"a\" as \"ChildId\", \"b\" as \"ParentId\" FROM cte;");
            migrationBuilder.Sql(query.ToString());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW public.\"UserNodeRelation\";");
        }
    }
}
