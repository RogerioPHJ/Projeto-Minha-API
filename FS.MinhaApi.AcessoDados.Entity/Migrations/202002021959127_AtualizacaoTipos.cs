namespace FS.MinhaApi.AcessoDados.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizacaoTipos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Alunoes", "Nome", c => c.String());
            AlterColumn("dbo.Alunoes", "Endereco", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Alunoes", "Endereco", c => c.Int(nullable: false));
            AlterColumn("dbo.Alunoes", "Nome", c => c.Int(nullable: false));
        }
    }
}
