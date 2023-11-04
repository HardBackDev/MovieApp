using Entities.Models;
using FluentMigrator;
using Shared;

namespace MovieApp.Migrations.PopulatingMigrations
{
    [Migration(3)]
    public class PopulatingDirectorTable : Migration
    {
        public static List<Guid> DirectorIds = new();

        static PopulatingDirectorTable()
        {
            for (int i = 0; i < 6; i++)
            {
                DirectorIds.Add(Guid.NewGuid());
            }
        }

        public override void Down()
        {
            
        }

        public override void Up()
        {
            Insert.IntoTable(Tables.DirectorTable)
                .Row(new 
                {
                    director_id = DirectorIds[0],
                    director_name = "Damien Leone",
                    director_age = 29,
                    director_bio = "In 2016, Terrifier was released. Among other credits, Leone wrote and directed the film, which eventually became a cult film and for which he was nominated for a Fangoria Chainsaw Award for Best Makeup FX.[7][8][9][10][11][12][13] In 2022, Leone returned for the sequel, Terrifier 2. Terrifier 2 was received favorably by critics and was a hit at the box office, grossing more than $15 million on a $250,000 budget.[14][15][16][17][18][19] Leone was again nominated for a Fangoria Chainsaw Award Best Makeup FX, for his work on Terrifier 2, which he later won. It was later announced that Leone was working on Terrifier 3 and the upcoming film, Stream.",
                    director_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f8/Damien_Leone_by_Gage_Skidmore.jpg/330px-Damien_Leone_by_Gage_Skidmore.jpg"
                })
                .Row(new 
                {
                    director_id = DirectorIds[1],
                    director_age = 36,
                    director_bio = "Quentin Jerome Tarantino (/ˌtærənˈtiːnoʊ/; born March 27, 1963) is an American film director, screenwriter, producer, actor, and author. His films are characterized by stylized violence, extended dialogue including a pervasive use of profanity, and references to popular culture.\r\n\r\nTarantino began his career as an independent filmmaker with the release of the crime film Reservoir Dogs in 1992. His second film, Pulp Fiction (1994), a dark comedy crime thriller, was a major success with critics and audiences winning numerous awards, including the Palme d'Or and the Academy Award for Best Original Screenplay. In 1996, he appeared in From Dusk till Dawn, also writing the screenplay. Tarantino's third film, Jackie Brown (1997), paid homage to blaxploitation films",
                    director_name = "Quentin Tarantino",
                    director_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Quentin_Tarantino_by_Gage_Skidmore.jpg/330px-Quentin_Tarantino_by_Gage_Skidmore.jpg"
                })
                .Row(new 
                {
                    director_id = DirectorIds[2],
                    director_age = 67,
                    director_name = "James Cameron",
                    director_bio = "James Francis Cameron CC (born August 16, 1954) is a Canadian filmmaker. A major figure in the post-New Hollywood era, Cameron is considered one of the industry's most innovative filmmakers, regularly making use of novel technologies. He first gained recognition for writing and directing The Terminator (1984) and found further success with Aliens (1986), The Abyss (1989), Terminator 2: Judgment Day (1991), and the action comedy True Lies (1994). He wrote and directed Titanic (1997), Avatar (2009) and its sequels, with Titanic earning him Academy Awards for Best Picture, Best Director, and Best Film Editing. A recipient of various other industry accolades, two of his films have been selected for preservation in the National Film Registry by the Library of Congress.",
                    director_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fe/James_Cameron_by_Gage_Skidmore.jpg/330px-James_Cameron_by_Gage_Skidmore.jpg"
                })
                .Row(new 
                {
                    director_id = DirectorIds[3],
                    director_age = 39,
                    director_name = "Joel Coen",
                    director_bio = "Joel Daniel Coen is an American filmmaker who regularly collaborates with his younger brother Ethan. They made Raising Arizona, Barton Fink, Fargo, The Big Lebowski, True Grit, O Brother Where Art Thou?, Burn After Reading, A Serious Man, Inside Llewyn Davis, Hail Caesar and other projects. Joel married actress Frances McDormand in 1984 and had an adopted son.",
                    director_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e2/Coen_brothers_Cannes_2015_2_%28CROPPED%29.jpg/330px-Coen_brothers_Cannes_2015_2_%28CROPPED%29.jpg"
                })
                .Row(new 
                {
                    director_id = DirectorIds[4],
                    director_age = 40,
                    director_name = "Marcus Dunstan",
                    director_bio = "Marcus Dunstan is an American screenwriter and director who, along with Patrick Melton, wrote screenplay for the film Feast, which was the winner of Season Three of the filmmaking competition reality TV series Project Greenlight. Dunstan has since written the screenplays for Feast, Feast II: Sloppy Seconds, Feast III: The Happy Finish, The Collector, The Collection, Saw IV, Saw V, Saw VI, and Saw 3D, and in some cases, making cameo appearances in those films as well.",
                    director_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8f/10.14.12MarcusDunstanByLuigiNovi.jpg/330px-10.14.12MarcusDunstanByLuigiNovi.jpg"
                });
        }
    }
}
