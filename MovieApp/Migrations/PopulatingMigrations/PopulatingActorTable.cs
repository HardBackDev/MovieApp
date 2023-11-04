using Entities.Models;
using FluentMigrator;
using Shared;

namespace MovieApp.Migrations
{
    [Migration(5)]
    public class PopulatingActorTable : Migration
    {
        public static List<Guid> ActorIds = new();

        static PopulatingActorTable()
        {
            for(int i = 0; i < 23; i++)
            {
                ActorIds.Add(Guid.NewGuid());
            }
        }

        public override void Down()
        {
        }

        public override void Up()
        {
            Insert.IntoTable(Tables.ActorTable)
                .Row(new 
                {
                    actor_id = ActorIds[0],
                    actor_name = "Thomas Jeffrey Hanks",
                    actor_bio = "Thomas Jeffrey Hanks (born July 9, 1956) is an American actor and filmmaker. Known for both his comedic and dramatic roles, he is one of the most popular and recognizable film stars worldwide, and is regarded as an American cultural icon.[2] Hanks' films have grossed more than $4.9 billion in North America and more than $9.96 billion worldwide,[3] making him the fourth-highest-grossing actor in North America.[4] He has received numerous honors including the AFI Life Achievement Award in 2002, the Kennedy Center Honor in 2014, the Presidential Medal of Freedom and the French Legion of Honor both in 2016,[5][6] as well as the Golden Globe Cecil B. DeMille Award in 2020.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Tom_Hanks_TIFF_2019.jpg/330px-Tom_Hanks_TIFF_2019.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[1],
                    actor_name = "Robin Gayle Wright",
                    actor_bio = "Robin Gayle Wright[1] (born April 8, 1966) is an American actress and director. She has received various accolades, including a Golden Globe Award, and nominations for eight Primetime Emmy Awards.\r\n\r\nWright first gained attention for her role as Kelly Capwell in the NBC Daytime soap opera Santa Barbara from 1984 to 1988. She transitioned to film with a starring role in the fantasy film The Princess Bride (1987), and she gained a nomination for a Golden Globe Award for Best Supporting Actress for her role in the top-grossing drama Forrest Gump (1994). She had further starring roles in the romantic drama Message in a Bottle (1999) and the thriller Unbreakable (2000), as she gained praise for her performances in the independent films Loved (1997), She's So Lovely (1997), Nine Lives (2005) and Sorry, Haters (2006). She has since taken on supporting roles in the sports drama Moneyball (2011), the thriller The Girl with the Dragon Tattoo (2011), the adventure film Everest (2015), the superhero film Wonder Woman (2017), and the science fiction film Blade Runner 2049 (2017).\r\n\r\nOn television, Wright starred in the HBO miniseries Empire Falls in 2005. From 2013 to 2018, she starred as Claire Underwood in the Netflix political drama series House of Cards. Her performance earned her a Golden Globe Award for Best Actress and six nominations for a Primetime Emmy Award for Outstanding Lead Actress. In 2016, Wright was named one of the highest paid actresses in the United States, earning US$420,000 per episode for House of Cards.[2] She has also directed ten episodes of the series as well as two episodes of the Netflix crime series Ozark in 2022.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/51/Robin_Wright_2009.jpg/330px-Robin_Wright_2009.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[2],
                    actor_name = "Gary Alan Sinise",
                    actor_bio = "Gary Alan Sinise is an American actor. Among other awards, he has won a Primetime Emmy Award, a Golden Globe Award, a Tony Award, and four Screen Actors Guild Awards. He has also received a star on the Hollywood Walk of Fame, and was nominated for an Academy Award. ",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/90/Gary_Sinise_2011_%28cropped%29.jpg/330px-Gary_Sinise_2011_%28cropped%29.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[3],
                    actor_name = "Lauren LaVera",
                    actor_bio = "LaVera's first leading role was as Sienna, a teenage girl that battles the supernatural serial killer Art the Clown in the 2022 slasher film Terrifier 2, alongside David Howard Thornton and Samantha Scaffidi.[3] The film was well-received by critics, with praise directed at LaVera's performance. IGN's review said that LaVera \"rules as Sienna in her angel-winged fantasy armor as a final girl fighting for family, facing her demons, and screaming bloody war cries in Art's mocking face,\"[4] while Matthew Jackson of Paste wrote that \"LaVera, tasked with injecting humanity into the sequel, lives up to this task with pure star power.\"[5] LaVera will reprise her role as Sienna in Terrifier 2.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Lauren_LaVera_AMFM_Studios.jpg/330px-Lauren_LaVera_AMFM_Studios.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[4],
                    actor_name = "Elliott Fullam",
                    actor_bio = "Elliott Fullam is an actor, known for his co-lead role as Jonathan in the critically acclaimed horror film Terrifier 2 as well as Sal the Rooster in the animated series Get Rolling with Otis on AppleTV. He also makes appearances in episodes of Instinct (CBS) and The Other Two (Comedy Central).",
                    actor_photo_url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTvF582DFm522swNp982-GTyFaf7zdj3zrhRw&usqp=CAU"
                })
                .Row(new 
                {
                    actor_id = ActorIds[5],
                    actor_name = "Morgan Freeman",
                    actor_bio = "Morgan Freeman is an American actor and producer. He is known for his distinctive deep voice and various roles in a wide variety of film genres. Throughout his career spanning over five decades, he has received numerous accolades, including an Academy Award, a Screen Actors Guild Award, and a Golden Globe Award.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/42/Morgan_Freeman_at_The_Pentagon_on_2_August_2023_-_230802-D-PM193-3363_%28cropped%29.jpg/330px-Morgan_Freeman_at_The_Pentagon_on_2_August_2023_-_230802-D-PM193-3363_%28cropped%29.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[6],
                    actor_name = "Timothy Francis Robbins",
                    actor_bio = "Timothy Francis Robbins (born October 16, 1958)[3] is an American actor, director and producer. He is best known for portraying Andy Dufresne in the film The Shawshank Redemption (1994), and has won a Best Supporting Actor Academy Award and Golden Globe for his role in Mystic River (2003) and another Golden Globe as Best Actor for The Player (1992).",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/61/Tim_Robbins_%28Berlin_Film_Festival_2013%29.jpg/330px-Tim_Robbins_%28Berlin_Film_Festival_2013%29.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[7],
                    actor_name = "David Howard Thornton",
                    actor_bio = "David Howard Thornton is an American actor. He is known for his role as Art the Clown, a role in which he appeared in Terrifier (2016), Mistress Peace Theatre (2020), Terrifier 2 (2022) and Bupkis (2023). He has had other various roles in film, television and video games, including Two Worlds II: Pirates of the Flying Fortress (2011), Ride to Hell: Retribution (2013), Invizimals: The Lost Kingdom (2013), Gotham (2017), The Bravest Knight (2019), The Exigency (2019), Alma's Way (2021) and starring as the Mean One in The Mean One (2022).",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/df/David_Howard_Thornton_by_Gage_Skidmore.jpg/330px-David_Howard_Thornton_by_Gage_Skidmore.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[8],
                    actor_name = "Jenna Kanell",
                    actor_bio = "Jenna Kanell (November 12, 1991) is an American actress, director, writer and stunt performer. She gained recognition for her portrayals of Tara Heyes in Terrifier (2016) and Kim Hansen in The Bye Bye Man (2017)—both horror films in which she performed her own stunt work. Her film career has since expanded to a variety of genres such as the political drama The Front Runner (2018), the horror-comedy Renfield (2023), and the action comedy Bad Boys 4 (2024).\r\n\r\nOn television, Kanell has made appearances on shows such as the Disney+ miniseries WandaVision (2021) and the Showtime anthology series The First Lady (2022).",
                    actor_photo_url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTSpy1TO0ciL35saKLDrdsA1pZ1r_E7GtnnhA&usqp=CAU"
                })
                .Row(new 
                {
                    actor_id = ActorIds[9],
                    actor_name = "Sam Worthington",
                    actor_bio = "Samuel Henry John Worthington (born 2 August 1976) is an Australian actor. He is best known for playing Jake Sully in the Avatar franchise, Marcus Wright in Terminator Salvation, and Perseus in Clash of the Titans and its sequel Wrath of the Titans. He has taken other dramatic roles, appearing in The Debt (2010), Everest (2015), Hacksaw Ridge (2016), The Shack (2017), Manhunt: Unabomber (2017), and Fractured (2019).",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2b/Avatar_The_Way_of_Water_Tokyo_Press_Conference_Sam_Worthington_%2852563252594%29_%28cropped%29.jpg/330px-Avatar_The_Way_of_Water_Tokyo_Press_Conference_Sam_Worthington_%2852563252594%29_%28cropped%29.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[10],
                    actor_name = "Zoe Saldaña",
                    actor_bio = "Zoë Yadira Saldaña-Perego (/sɑːlˈdænə/ sahl-DAN-ə,[2] Spanish: [ˈsoe salˈdaɲa]; née Saldaña Nazario; born June 19, 1978) is an American actress. Known primarily for her work in science fiction film franchises, she has starred in four of the highest-grossing films of all time (Avatar, Avatar: The Way of Water, Avengers: Infinity War, and Avengers: Endgame), a feat not achieved by any other performer.[3] Films she has appeared in have grossed more than $14 billion worldwide and, as of 2023, she is the second-highest-grossing film actress, and the fourth actor overall.[4][5] Time magazine named her one of the 100 most influential people in the world in 2023.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Avatar_The_Way_of_Water_Tokyo_Press_Conference_Zoe_Salda%C3%B1a_%2852563431865%29_%28cropped2%29.jpg/330px-Avatar_The_Way_of_Water_Tokyo_Press_Conference_Zoe_Salda%C3%B1a_%2852563431865%29_%28cropped2%29.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[11],
                    actor_name = "Kate Winslet",
                    actor_bio = "Kate Elizabeth Winslet CBE (/ˈwɪnzlət/;[2] born 5 October 1975) is an English actress.[3] Known for her work in independent films, particularly period dramas, and for her portrayals of headstrong and complicated women, she has received numerous accolades, including an Academy Award, a Grammy Award, two Primetime Emmy Awards, five BAFTA Awards and five Golden Globe Awards. Time magazine named Winslet one of the 100 most influential people in the world in 2009 and 2021. She was appointed Commander of the Order of the British Empire (CBE) in 2012.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Kate_Winslet_at_the_2017_Toronto_International_Film_Festival_%28cropped%29.jpg/330px-Kate_Winslet_at_the_2017_Toronto_International_Film_Festival_%28cropped%29.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[12],
                    actor_name = "Josh Stewart",
                    actor_bio = "Joshua Regnall Stewart (born February 6, 1977) is an American actor who is best known for his role as Holt McLaren in the FX TV series Dirt and as Detective William LaMontagne, Jr., on the CBS series Criminal Minds. He was also cast as Brendan Finney in the final season of the NBC TV series Third Watch and as Barsad in Christopher Nolan's The Dark Knight Rises. Other roles include War Machine and a major antagonist in Netflix's The Punisher (2019).",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/10.14.12JoshStewartByLuigiNovi1.jpg/330px-10.14.12JoshStewartByLuigiNovi1.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[13],
                    actor_name = "Madeline Zima",
                    actor_bio = "Madeline Zima (born September 16, 1985) is an American actress. She portrayed Grace Sheffield on the CBS sitcom The Nanny (1993–1999), Mia Lewis on the Showtime comedy drama series Californication (2007–2011), and Gretchen Berg on the NBC series Heroes (2009–2010).",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2e/Madeline_Zima_in_2008.jpg/270px-Madeline_Zima_in_2008.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[14],
                    actor_name = "Juan Fernández de Alarcón",
                    actor_bio = "Juan de Jesús Fernández de Alarcón (born December 13, 1956) is a Dominican actor best known for playing antagonist roles in movies.\r\n\r\nFernández was born in Santo Domingo, Dominican Republic. He made his movie debut in Salome[1] and has gone to star in over 30 movies.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/90/Juan_Fern%C3%A1ndez2.jpg/330px-Juan_Fern%C3%A1ndez2.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[15],
                    actor_name = "Frances McDormand",
                    actor_bio = "Frances Louise McDormand (born Cynthia Ann Smith; June 23, 1957) is an American actress and producer. Throughout her career spanning over four decades, she has gained acclaim for her roles in small-budget independent films. McDormand has received numerous accolades, including four Academy Awards, two Emmy Awards, and one Tony Award, making her one of the few performers to achieve the \"Triple Crown of Acting\". Additionally, she has received three BAFTA Awards, two Golden Globe Awards, and four Screen Actors Guild Awards.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ed/Frances_McDormand_2015_%28cropped%29.jpg/330px-Frances_McDormand_2015_%28cropped%29.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[16],
                    actor_name = "Steven Vincent Buscemi",
                    actor_bio = "Steven Vincent Buscemi (/buːˈsɛmi/ boo-SEM-ee,[1][2][Note 1] Italian: [buʃˈʃɛːmi]; born December 13, 1957) is an American actor. Buscemi is known for his work as an acclaimed character actor.[3][4] His early credits consist of major roles in independent film productions such as the AIDS drama Parting Glances (1986), Mystery Train (1989), In the Soup (1992), and his breakout role as Mr. Pink in Quentin Tarantino's Reservoir Dogs (1992).",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Steve_Buscemi_crop.jpg/330px-Steve_Buscemi_crop.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[17],
                    actor_name = "William H. Macy",
                    actor_bio = "William Hall Macy Jr. (born March 13, 1950) is an American actor. His film career has been built on appearances in small, independent films, though he has also appeared in mainstream films.[3] Some of his best known starring roles include those in Fargo (1996), Air Force One (1997), Boogie Nights (1997), Magnolia (1999), Jurassic Park III (2001), Seabiscuit (2003), Thank You for Smoking (2005), and The Lincoln Lawyer (2011). Macy has won two Emmy Awards and four Screen Actors Guild Awards, while his performance in Fargo earned a nomination for the Academy Award for Best Supporting Actor. From 2011 to 2021, he played Frank Gallagher, a main character in Shameless, the Showtime adaptation of the British television series. Macy has been married to Felicity Huffman since 1997.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/WilliamHMacyTIFFSept2012.jpg/330px-WilliamHMacyTIFFSept2012.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[18],
                    actor_name = "Leonardo DiCaprio",
                    actor_bio = "Leonardo Wilhelm DiCaprio (/diˈkæprioʊ/, /dɪ-/; Italian: [diˈkaːprjo]; born November 11, 1974) is an American actor and film producer. Known for his work in biographical and period films, he is the recipient of numerous accolades, including an Academy Award, a British Academy Film Award and three Golden Globe Awards. As of 2019, his films have grossed over $7.2 billion worldwide, and he has been placed eight times in annual rankings of the world's highest-paid actors.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/46/Leonardo_Dicaprio_Cannes_2019.jpg/330px-Leonardo_Dicaprio_Cannes_2019.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[19],
                    actor_name = "Tohoru Masamune",
                    actor_bio = "Tohoru Masamune (born 18 November 1959) is an American actor. His roles include Shredder in the 2014 Michael Bay film Teenage Mutant Ninja Turtles, Kevin Hall-Yoshida (Paxton's father) in Never Have I Ever, and Inception.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Tohoru_Masamune_at_2019_Emmy_Awards_Cropped.jpg/330px-Tohoru_Masamune_at_2019_Emmy_Awards_Cropped.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[20],
                    actor_name = "Quentin Tarantino",
                    actor_bio = "Quentin Jerome Tarantino (/ˌtærənˈtiːnoʊ/; born March 27, 1963) is an American film director, screenwriter, producer, actor, and author. His films are characterized by stylized violence, extended dialogue including a pervasive use of profanity, and references to popular culture.",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Quentin_Tarantino_by_Gage_Skidmore.jpg/330px-Quentin_Tarantino_by_Gage_Skidmore.jpg"
                })
                .Row(new 
                {
                    actor_id = ActorIds[21],
                    actor_name = "John Travolta",
                    actor_bio = "John Joseph Travolta (born February 18, 1954)[1][2] is an American actor. He became prominent during the 1970s, appearing on the television sitcom Welcome Back, Kotter (1975–1979) and starring in the box office successes Carrie (1976), Saturday Night Fever (1977), Grease (1978), and Urban Cowboy (1980).",
                    actor_photo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/John_Travolta_Cannes_2018_%28cropped%29.jpg/330px-John_Travolta_Cannes_2018_%28cropped%29.jpg"
                });

        }
    }
}
