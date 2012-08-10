using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using GamePedia.DataModel;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace GamePedia.Data
{
    public enum GroupType
    {
        Genre,
        Console,
        GroupBase,
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// </summary>
    public sealed class GamePediaDataSource
    {
        private static GamePediaDataSource _GamePediaDataSource = new GamePediaDataSource();

        private ObservableCollection<GamePediaDataGroup> _allGroups = new ObservableCollection<GamePediaDataGroup>();
        public ObservableCollection<GamePediaDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public ObservableCollection<GamePediaDataConsole> AllConsoles
        {
            get
            {
                var consoles = this._allGroups.Where(x => x is GamePediaDataConsole).Cast<GamePediaDataConsole>();
                return new ObservableCollection<GamePediaDataConsole>(consoles);
            }
        }

        public ObservableCollection<GamePediaDataGenre> AllGenres
        {
            get
            {
                var genres = this._allGroups.Where(x => x is GamePediaDataGenre).Cast<GamePediaDataGenre>();
                return new ObservableCollection<GamePediaDataGenre>(genres);
            }
        }

        public static IEnumerable<GamePediaDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            return _GamePediaDataSource._allGroups;
        }

        public ObservableCollection<GamePediaDataGroupBase> AllProducers
        {
            get
            {
                var producers = this._allGroups.Where(x => x is GamePediaDataGroupBase).Cast<GamePediaDataGroupBase>();
                return new ObservableCollection<GamePediaDataGroupBase>(producers);
            }
        }

        public static GamePediaDataGenre GetGenre(string uniqueId)
        {
            return _GamePediaDataSource.AllGenres.FirstOrDefault(x => x.UniqueId == uniqueId);
        }

        public static IEnumerable<GamePediaDataGenre> GetGenres(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _GamePediaDataSource.AllGenres;
        }

        public static GamePediaDataConsole GetConsole(string uniqueId)
        {
            return _GamePediaDataSource.AllConsoles.FirstOrDefault(x => x.UniqueId == uniqueId);
        }

        public static GamePediaDataGroupBase GetProducter(string uniqueId)
        {
            return _GamePediaDataSource.AllProducers.FirstOrDefault(x => x.UniqueId == uniqueId);
        }

        public static IEnumerable<GamePediaDataGroupBase> GetProducers(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _GamePediaDataSource.AllProducers;
        }

        public static GamePediaDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _GamePediaDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static GamePediaDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _GamePediaDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            return matches.FirstOrDefault();
        }

        public GamePediaDataSource()
        {
            //String ITEM_CONTENT = String.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
            //            "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            GamePediaDataGroupBase sega;
            GamePediaDataConsole megadrive;
            CreateSegaAndConsoles(out sega, out megadrive);

            GamePediaDataGroupBase sony;
            GamePediaDataConsole playStation3;
            GamePediaDataConsole playStation2;
            GamePediaDataConsole playStation;
            CreateSonyAndConsoles(out sony, out playStation3, out playStation2, out playStation);

            GamePediaDataGroupBase microsoft;
            GamePediaDataConsole xbox360;
            GamePediaDataConsole xbox;
            GamePediaDataConsole pc;
            CreateMicrosoftAndConsoles(out microsoft, out xbox360, out xbox, out pc);

            GamePediaDataGroupBase nintendo;
            GamePediaDataConsole snes;
            GamePediaDataConsole wii;
            GamePediaDataConsole n64;
            CreateNintendoAndConsoles(out nintendo, out snes, out wii, out n64);

            GamePediaDataGroupBase genres;
            GamePediaDataGenre action;
            GamePediaDataGenre adventure;
            GamePediaDataGenre rpg;
            GamePediaDataGenre race;
            GamePediaDataGenre fight;
            GamePediaDataGenre sports;
            CreateGenres(out genres, out action, out adventure, out rpg, out race, out fight, out sports);

            var pes6 = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "Pro Evolution Soccer 6",
                "Assets/Item/pes6.jpg",
                "Pro Evolution Soccer 6 (também conhecido como Winning Eleven 10 e Winning Eleven X para Xbox 360 no Japão, Winning Eleven: Pro Evolution Soccer 2007 nos Estados Unidos) é um jogo de vídeo desenvolvido e publicado pela Konami. Lançado em 27 de Outubro de 2006 para o PlayStation 2, Xbox 360, e as plataformas PC e depois no Nintendo DS e PlayStation Portable em 1 de Dezembro de 2006, Pro Evolution Soccer 6 é a 6 ª edição da série Pro Evolution Soccer para a PlayStation 2, 2 para a PlayStation Portable e 4 para PC. É o primeiro jogo para a estréia no Nintendo DS e Xbox 360. A versão Xbox 360 tem gráficos melhorados, mas mantém uma jogabilidade semelhante aos demais versões do console",
                //content
                "PES6 marks the first time the International Challenge Mode has been included on the PES Series. Usually this is seen on the Japanese version - Winning Eleven - where you play as Japan and take them through the qualifiers to the International Cup and then attempt to win it. On PES, however, you have the ability to choose any playable nation on the game. The user can only play the qualifiers from Europe, Asia, South America, and North/Central America.",
                sony, sports, playStation2, pc, xbox360);

            var haloce = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "Halo: Combat Evolved",
                "Assets/Item/haloce.jpg",
                "Halo: Combat Evolved, também conhecido como Halo: CE ou, simplesmente, Halo, é um jogo de tiro em primeiro-pessoa desenvolvido pela Bungie e publicado pela Microsoft Game Studios. O primeiro jogo da franquia Halo, foi lançado em 15 de novembro de 2001 como um título de lançamento para o sistema de jogos Xbox, e é considerado \"killer app\" da plataforma. Com mais de cinco milhões de cópias vendidas em todo o mundo a partir de 09 de novembro de 2005, a Microsoft lançou versões do jogo para Microsoft Windows (portado pela Gearbox Software) e Mac OS X em 2003, eo enredo envolvente foi adaptado e elaborado em uma série de romances e quadrinhos. O jogo foi depois lançado como um Xbox original para download em um Xbox 360 HDD.",
                //content
                "Como um jogo de tiro em primeira pessoa, a jogabilidade de Halo: Combat Evolved é fundamentalmente semelhante à dos seus pares, com foco em combate em um ambiente 3D que é vista quase totalmente de vista de um personagem olho. O jogador pode se mover e olhar para cima, baixo, esquerda ou direita. O jogo apresenta veículos, que vão desde jipes blindados e tanques para hovercraft estranho e aviões, muitos dos quais pode ser controlada pelo jogador. O jogo muda para uma perspectiva de terceira pessoa durante o uso do veículo para os pilotos e operadores de armas montado; passageiros manter uma visão em primeira pessoa. A tela do jogo heads-up inclui um \"rastreador de movimento\" que registra movimento aliados, inimigos e veículos em um determinado raio do jogador.\n" +
                "O personagem do jogador é equipado com um escudo de energia que absorve dano de armas de fogo e impactos fortes. Carga do escudo aparece como uma barra azul no canto da tela do jogo heads-up, e ele automaticamente recarrega, se nenhum dano é sustentado por um breve período. Quando o escudo está completamente esgotado, o jogador é altamente vulnerável, e mais danos reduz os pontos de vida de um medidor de saúde secundário. [9] Quando este medidor de saúde chega a zero, o personagem morre e as recargas de jogo a partir de um checkpoint salvo. Saúde pode ser reabastecido através da coleção de pacotes de saúde espalhados em torno dos níveis do jogo, mas a introdução do jogo de um mecânico escudo de regeneração representou uma partida de jogos FPS da época.\n" +
                "Arsenal de Halo consiste de armas de ficção científica. O jogo foi elogiado por dar cada arma um propósito único, tornando cada úteis em cenários diferentes. Por exemplo, armas de plasma precisam de tempo para esfriar se despedido muito rapidamente, mas não pode ser recarregado e deve ser descartado após o esgotamento de suas baterias, enquanto armas de fogo convencionais não podem superaquecer, mas exigem recarga e munição. Em contraste com os estoques de armas grandes de jogos contemporâneos FPS, os jogadores de Halo pode transportar apenas duas armas ao mesmo tempo, chamando para a estratégia na gestão de armas de fogo",
                microsoft, action, xbox, pc);

            var nfl2002 = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "NFL Fever 2002",
                "Assets/Item/nfl2002.jpg",
                "NFL Fever 2002 é um jogo de Futebol Americano vídeo publicado e desenvolvido pela Microsoft Game Studios. Ele foi originalmente lançado em 15 de novembro de 2001, para o console de videogame Xbox. O jogo foi seguido pela NFL Fever 2003",
                //Content
                "Fever offers many game modes, including Season and Dynasty. In Dynasty mode, you guide one or more teams through multiple seasons, managing every aspect of your team. Rosters, positions, build the dream team that everyone fears.",
                microsoft, sports, xbox);

            var gow3 = new GamePediaDataItem(Guid.NewGuid().ToString(),
                    "God of War 3",
                    "Assets/Item/gow3.jpg",
                    "God of War III é uma terceira pessoa jogo de ação-aventura desenvolvido pela SCE Santa Monica Studio e publicado pela Sony Computer Entertainment. Primeiramente lançado para o console PlayStation 3 de vídeo em 16 de março de 2010, o jogo é o quinto da série God of War e a sequela de God of War e God of War II. O jogo apresenta um sistema de magia renovada, um aumento no número de inimigos na tela, maior interação com o meio ambiente, novos ângulos de câmera, e conteúdo para download. Como o jogo mais vendido na série God of War, o jogo já vendeu mais de 5,2 milhões de cópias em todo o mundo em junho de 2012 e será lançado em 28 de agosto de 2012 como parte do Deus da Saga Guerra, também no PlayStation 3.",
                    //Content
                    "God of War III apresenta jogabilidade semelhante às parcelas anteriores. Um jogo single player, é apresentado com uma câmera fixa a partir da visão em terceira pessoa com o jogador controlando os Kratos de caracteres em uma combinação de combate, tanto normal e em tempo rápido, com plataformas e elementos de jogo de quebra-cabeça. O jogo também apresenta uma visão de câmera em primeira pessoa durante duas batalhas (a primeira da série). Armas de Kratos principais são as Blades of Exile (inicialmente as Blades of Athena), com outras armas novas, incluindo as garras de Hades, o Cestus Neméia ea Nemesis Whip. O Cestus Neméia (um par de luvas de punho), e o chicote Nemesis são cruciais para o jogo, como eles são obrigados a avançar através de determinadas fases do jogo.\n"+
                    "Ao contrário dos jogos anteriores da série, as armas primárias também ditam o uso da magia, com cada arma ter uma habilidade mágica individual: Divine ReckoningBlades of Athena), Army of Sparta (Blades of Exile), Soul Sumon (Claws of Hades ), Nemean Roar (Nemean Cestus) e Rage Nemesis (Nemesis Whip). O poder de cada um aumenta capacidade mágica através de atualizar a arma para que cada um está ligado Kratos também tem uma variedade de armas secundárias referido como \"itens\" que têm uso limitado antes de precisar recarregar (o que ocorre automaticamente), sendo o arco de Apolo, a cabeça de Hélios e Botas de Hermes. Todos os três são obrigados a avançar através de determinadas fases do jogo.\n" +
                    "Kratos também adquire outro item \"passivo\" similar a um Tridente de Poseidon: a alma de Hades, que permite que Kratos a nadar no rio Styx sem ser atacado por almas perdidas. Gorgon Eyes e penas de Phoenix, que aumentam a quantidade máxima de Saúde e Magic, respectivamente, retornam, embora neste caso, apenas três olhos ou penas (em oposição aos seis original) precisam ser encontradas para aumentar a máxima do medidor. Minotaur Horns foram adicionados para aumentar o medidor de itens e requerem três chifres para aumentar máxima do medidor. Os olhos, penas e chifres são encontrados em um estilo de peito que difere da Saúde normal e magia dando o peito. Outros baús encontrados no jogo, contendo orbs, são marcados com uma cor correspondente para as esferas (verde, azul e vermelho). Green Orbs recuperam a saúde, orbs azuis reabastecer orbs mágicos e vermelho proporcionar uma experiência, que por sua vez permite a modernização de armas (por exemplo, novos e ataques mais poderosos). Orbs brancas também estão disponíveis que reabastecer o medidor de raiva",
                    sony, action, playStation3);

            var asura = new GamePediaDataItem(Guid.NewGuid().ToString(),
                    "Asura's Wrath",
                    "Assets/Item/Asura.png",
                    //Description
                    "É uma jogo de ação criado entre a CyberConnect2 e a Capcom. Foi anunciado pela primeira vez em 2010 na Tokyo Game Show.\n" + 
                    "Foi desenvolvido para ser lançado no Japão, América do Norte e Europa para Xbox 360 e PlayStation 3.\n" +
                    "Foi lançado em 21 de fevereiro de 2012 na América do Norte e 24 de fevereiro de 2012 na Europa. \n" +
                    "De acordo com o jogo do produtor Kazuhiro Tsuchiya: \"Asura's Wrath leva elementos da mitologia hundo e combina-os com ficção científica. No jogo, Asura é um semideus lutando para recuperar a filha das divindades que a raptaram e o baniram do mundo mortal.\"",
                    //Content
                    "A jogabilidade de Asura Wrath é uma combinação de vários gêneros, enquanto que em geral é apresentado no estilo de uma série de anime episódico. A jogabilidade em todo turnos entre uma ação de terceira pessoa e um jogo de tiro ferroviário. O jogo também requer participação direta do jogador durante os eventos cinematográficos na forma de cenas interativas com evento em tempo "+
                    "rápido e vários botão sensível ao contexto pede. Em todas as formas de jogo no entanto, o progresso jogador é determinada por duas bitolas representados na parte superior da tela, a bitola vida e de ruptura. O medidor de vida determina a saúde atual e dano recebido pelo personagem que se esgotado resulta em um game over / reiniciar tela para que a seção atual. O medidor de rebentamento "+
                    "no entanto começa vazia no início de cada encontro que necessita de ser totalmente carregada. Para fazer isso os jogadores devem derrotar os inimigos com sucesso, infligir grandes quantidades de dano e pressione o atual em tempo rápido pronta corretamente e no tempo. Uma vez preenchido ao máximo, os jogadores podem desencadear um ataque de estouro poderoso, que na maioria dos casos é "+
                    "necessária para acabar com adversários fortes e avançar o enredo / jogabilidade, até mesmo começar um outro cutscene. Para além destes dois aparelhos de medição, um um adicional conhecida como \"Unlimited gauge\" enche-se de uma forma semelhante à bitola de ruptura, mas em vez disso pode ser activado para temporalmente aumentar os danos que podem ser causados ​​aos oponentes.\n" +
                    "As sequências de acção na terceira pessoa se assemelham \"beat 'em up\" estilo de jogo onde o jogador deve derrotar os inimigos em combate corpo a corpo, utilizando ataques leves e pesados, contadores, traços e projéteis. Enquanto regulares ataques leves são rápidos, os ataques mais pesados ​​infligir mais danos e pode jogar para trás vários inimigos ainda podem superaquecer exigindo um período de arrefecimento entre os usos. Os jogadores também podem realizar movimentos contrários se o prompt de entrada de corrente durante um ataque inimigo. Quando um inimigo é derrubado, movimentos especiais podem ser realizados que ajuda ainda mais encher o medidor de explosão. Se, no entanto o jogador está batido para trás, eles têm a chance de se recuperar rapidamente por landng em seus pés e salvar a saúde suplementar. A porção rail shooter do jogo envolve o jogador ainda se movendo sobre um eixo fixo, sendo apenas capaz de se mover para esquivar e manobrar contra ataques de entrada e de obstáculos, o tempo todo no bloqueio e atirar contra os inimigos.\n" +
                    "O elemento interativo cutscene é integrado com a jogabilidade no entanto. Entradas corretas quando solicitado vai avançar a história, enquanto falha pode causar o reinício de uma seqüências e danos à saúde em uma seqüência de jogo anterior. Enquanto algumas seqüências podem continuar independentemente, alguns quick-time eventos têm graus de sucesso onde o jogador pode tentar pressionar em um momento ainda mais específico do que quando o prompt imediatamente e aparece inicialmente. Por exemplo, uma imprensa demasiado cedo ou mais tarde pode registrar um simples \"bom\" ou \"grande\", enquanto o exato momento correto irá registar-se como \"excelente\". O desempenho do jogador neste aspecto, juntamente com o tempo levado para danos infligidos completa e global é classificada no final de cada episódio, com o maior grau de ser um \"Rank S\". Um certo número de Ranks S desbloquear o final \"verdadeiro final\" escondido do jogo.",
                    sony, adventure, action, xbox360, playStation3);

            var ffIX = new GamePediaDataItem(Guid.NewGuid().ToString(),
                    "Final Fantasy IX",
                    "Assets/Item/ffIX.jpg",
                    "Final Fantasy IX é um jogo RPG desenvolvido e publicado pela Square (agora Square Enix) para a consola de jogos Sony PlayStation. Originalmente lançado em 2000, é o nono título da série Final Fantasy e por último a estrear na PlayStation. Em 2010, foi re-lançado como um título Clássicos PSOne na PlayStation Network. O jogo introduziu novos recursos para a série como o 'Evento Active Time', 'Mognet', e um equipamento exclusivo e sistema de habilidade.\n" + 
                    "Enredo Final Fantasy IX, gira em torno de uma guerra entre muitas nações. Jogadores seguem um jovem ladrão chamado Zidane Tribal, que se junta com os outros para derrotar a Rainha Brahne de Alexandria, um dos responsáveis ​​pelo início da guerra. As mudanças de enredo, no entanto, quando os personagens percebem que Brahne está trabalhando com uma pessoa ainda mais perigoso, chamado Kuja",
                    
                    "Em Final Fantasy IX, o jogador navega um personagem ao longo do jogo, explorando áreas e interagir com personagens não-jogadores. A maior parte do jogo ocorre em cidades e masmorras que são referidos como \"telas de campo\". Para auxiliar a exploração na tela de campo, Final Fantasy IX apresenta o ícone \"campo\", um ponto de exclamação aparece sobre a cabeça de seu personagem principal, sinalizando um item ou sinal fica nas proximidades. Jogadores falam com moogles para gravar seu progresso, restaurar a energia da vida com uma barraca e compra itens, um desvio das parcelas anteriores, que utilizavam um save point para executar essas funções. Moogles podem solicitar o personagem jogável entregar cartas a outros Moogles através Mognet; personagens jogáveis ​​também pode receber cartas de personagens não-jogáveis.\n" +
                    "Jogadores viagem entre os locais de campo na tela o mapa do mundo, uma representação três, dimensional reduzido do mundo Final Fantasy IX, apresentado a partir de uma perspectiva de cima para baixo. Os jogadores podem navegar livremente ao redor da tela mapa do mundo a não ser restringido por terreno, como corpos de água ou cordilheiras. Para superar as limitações geográficas, os jogadores podem montar chocobos, navegar em um barco ou dirigíveis piloto. Como parcelas anteriores de Final Fantasy, viagens em toda a tela mapa do mundo e locais de tela hostis campo é interrompida por encontros inimigos aleatórios.\n" +
                    "Final Fantasy IX offers a new approach to town exploration with Active Time Events, which provide character development, special items and prompts for key story-altering decisions.[4] At specific points, the player may view events that are occurring simultaneously. ATE is occasionally used to simultaneously control two teams when the party is divided to solve puzzles and navigate mazes",
                    sony, rpg, playStation);

            var sonic = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "Sonic the Hedgehog",
                "Assets/Item/sonic.png",
                "Sonic the Hedgehog é um jogo da série best-seller de vídeo lançado pela Sega estrelando e nomeado após a sua personagem mascote, Sonic the Hedgehog. A série começou em 1991 com o lançamento de Sonic the Hedgehog na Unidade de Sega Genesis / Mega, que foi responsável por transformar Sega em uma empresa líder de vídeo game durante a era 16-bit no início e meados da década de 1990.",
                //content
                "Quase todos os jogos da série apresenta um ouriço azul chamado Sonic como o personagem do jogador central e protagonista. O detalhe jogos Sonic e tentativa de seus aliados para salvar o mundo de várias ameaças, principalmente o mal gênio Dr. Ivo \"Eggman\" Robotnik, o principal antagonista da série. Robotnik objetivo é o de governar a Terra, para conseguir isso, ele geralmente tenta eliminar Sonic e para adquirir as poderosas Esmeraldas do Caos.", 
                sega, megadrive, adventure);

            var superSmashBros = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "Super Smash Bros. Brawl",
                "Assets/Item/ssbb.jpg",
                "Super Smash Bros Brawl, muitas vezes abreviado como SSBB ou simplesmente como Brawl, é o terceiro no Super Smash Bros série de jogos de crossover de luta, desenvolvido por uma equipe de desenvolvimento ad hoc constituído por Sora, Game Arts e funcionários de outros desenvolvedores, e publicado pela Nintendo para o console de videogame Wii.",
                //content
                "Após seus antecessores, Brawl utiliza um sistema de batalha ao contrário do que jogos de luta típicas. Os jogadores podem escolher entre uma grande seleção de personagens, cada um tentar bater os seus adversários para fora da tela como eles lutam em diversas fases. Os personagens de Brawl incluem a maioria dos mesmos como os antecessores, como o Mario bem conhecido e Pikachu. Em vez de usar as barras de saúde tradicionais que começam em um valor máximo e perder valor, os personagens de pancadaria começa o jogo com 0%;. O valor aumenta à medida que eles tomam dano e pode subir mais de 100% até um máximo de 999% [18] Como aumentos percentuais personagem, o personagem voa mais para trás quando atingido. Quando um personagem é nocauteado além fronteira de um palco e desaparece da tela, o personagem perde quer uma vida, um ponto, ou moedas, dependendo do modo de jogo. [19] Brawl inclui uma função que permite aos jogadores criar perfis com personalizado configurações do botão para cada método de controle juntamente com o seu nome de usuário escolhido.\n" +
                "Os personagens de Brawl lutam entre si usando uma variedade de ataques, que dão ao jogador uma seleção mais ampla do que os antecessores. Os jogadores executar cada movimento, premindo um botão em conjunto com uma inclinação da vara de controlo ou uma prensa de a D-almofada, dependendo do modo de controlo. Além dos ataques básicos, os personagens têm acesso a movimentos mais poderosos, conhecidos como ataques de quebra. Cada personagem tem quatro movimentos únicos, que muitas vezes causam efeitos além de dano a um oponente. Brawl introduz a capacidade de executar caracteres específicos ataques super, referidos como \"Final Smash\" se move. Significativamente mais potente do que os ataques regulares, estes movimentos têm uma grande variedade de efeitos que vão desde cerca de blastos inevitáveis ​​a transformações temporários. Movimentos Final Smash pode ser realizada por destruir uma Smash Ball: um colorido, brilhante, item orb-like com o logotipo Smash Bros que flutua em torno de cada etapa de vez em quando, dependendo da seleção de itens que foram estabelecidos antes do início da partida.", 
                nintendo, wii, fight, action);
            
            var topGear2 = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "Top Gear 2",
                "Assets/Item/topgear2.jpg",
                "Top Gear 2 (conhecido como Top Racer 2 no Japão) foi a sequela para o Game Gear 1992 Top, lançado em 01 de janeiro de 1993 para o Super NES, 24 de maio de 1994 para a unidade de Sega Genesis / Mega e em 1994 para Amiga. Ele foi desenvolvido pela Gremlin Interactive e publicado pela Kemco para o Super NES e pela Vic Tokai para a unidade de Genesis/Mega.",
                //content
                "Nesta sequela, o jogo torna-se mais realista, com um diagrama de lesão no lado esquerdo da tela, os carros mais lentos, ea possibilidade de atualizar sua máquina. Os carros se tornam mais difíceis de manusear e os adversários são mais rápidos e mais resistente do que no jogo anterior. A nova adição de tempo também desempenha um papel, forçando o jogador a mudar de seco para pneus de chuva.\n" +
                "O jogo tem lugar em 16 países, incluindo 64 cidades, começando com Australásia (Austrália e Nova Zelândia). Depois de cada país é batido, o jogador recebe uma senha, que pode ser usado mais tarde para pegar o jogo de volta se a partir dessa posição. Por causa do acordo de naming rights, Giza Necropolis (no país do Egito) foi renomeado para Hugh Sitton, um fotógrafo da Corbis Corporation.\n" +
                "A jogabilidade é bastante simples, existe um mapa que mostra que as direções das curvas seguintes será, e é dado ao jogador 6 boosts \"Nitro\" no início, que aumentar dramaticamente a velocidade de carros por um curto período de tempo. Em alguns cursos há pickups ao longo da estrada que vão desde um \"$\", que é dinheiro de $ 1.000, um \"N\", que é um extra \"impulso nitro\" e um \"S\" que é um nitro automática administrada logo quando pegou e retorna cada volta. Os captadores outros só podem ser recolhidas uma vez por corrida.", 
                nintendo, snes, megadrive, race);

            var mario64 = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "Super Mario 64",
                "Assets/Item/mario64.jpg",
                "Super Mario 64 é um jogo de plataforma, publicado pela Nintendo e desenvolvido por sua divisão de EAD, para o Nintendo 64. Junto com Pilotwings 64, era um dos títulos de lançamento para o console. Foi lançado no Japão em 23 de junho de 1996, e mais tarde na América do Norte, Europa e Austrália. Super Mario 64 já vendeu mais de onze milhões de cópias. Um remake melhorado chamado Super Mario 64 DS foi lançado para o Nintendo DS em 2004.",
                //content
                "Como um dos primeiros três jogos de plataforma tridimensional (3D), Super Mario 64 possui livre-vagueando graus analógicos de liberdade, grandes áreas abertas, e verdadeiros polígonos 3D ao invés de sprites bidimensionais (2D). Aclamado como \"revolucionário\", o jogo deixou uma impressão duradoura sobre design de jogos 3D, particularmente notável por sua utilização de um sistema de câmera dinâmica e da execução do seu controle analógico.\n" +
                "Super Mario 64 é um jogo de plataformas 3D onde o jogador controla Mario através de vários cursos. Cada curso é um mundo fechado em que o jogador está livre para passear em todos os sentidos e descobrir o ambiente sem limites de tempo. Os mundos são preenchidos com os inimigos que atacam Mario, assim como criaturas amigáveis ​​que prestam assistência, informações, oferta ou pedir um favor (como rosa \"amantes da paz\" Bob-omb Buddies). O jogador reúne estrelas em cada curso; algumas estrelas só aparecerão depois de completar determinadas tarefas, muitas vezes sugerido por o nome do curso. Esses desafios incluem derrotar um chefe, resolver enigmas, correndo de um adversário, e recolher moedas. Como mais estrelas são coletadas, mais áreas do mundo hub castelo se tornam acessíveis. O jogador desbloqueia as portas no castelo com teclas obtidos ao derrotar Bowser em cursos especiais. Há muitos escondidos mini-cursos e outros segredos para o jogo, a maioria contendo estrelas extras necessários para completar o jogo completamente.\n" +
                "Alguns cursos têm power-ups tampa especial que aumentam as habilidades de Mario. O Cap asa permite que Mario voar; a tampa de metal faz com que ele imune a maior parte dos danos, que lhe permite suportar o vento, andar debaixo d'água, e será afetada por gases nocivos, eo Vanish Cap torna-o parcialmente imaterial e lhe permite atravessar alguns obstáculos tais como malha de arame, bem como a concessão de invulnerabilidade a algumas formas de danos. Alguns cursos contêm canhões que Mario pode acessar falando com um Bob-omb rosa Buddy. Depois de introduzir um canhão, Mario pode ser disparado de chegar a lugares distantes. Quando o jogador tem a Cap Asa equipada, canhões pode ser usado para atingir altitudes elevadas ou voar na maioria dos níveis rapidamente.",
                nintendo, n64, adventure);

            var alteredBeast = new GamePediaDataItem(Guid.NewGuid().ToString(),
              "Altered Beast",
              "Assets/Item/alteredbeast.png",
              "Altered Beast é um jogo de videogame lançado em 1988 pela Sega para Arcade e que teve versões para vários consoles, entre eles Mega Drive, Master System e Game Gear. Foi um dos primeiros jogos para o Mega Drive e ganhou uma versão para o Playstation 2 em Janeiro de 2005.",
                //content
              "O jogo possui cinco fases em side-scroll (movimento lateral da tela) e ao final de cada fase deve enfrentar o vilão Neff transformado em um monstro horrendo e mortal, isto desde que o jogador já estivesse transformado em besta (caso ainda não estivesse nesta forma, quando se encontrava com Neff, este fugia e a fase continuava até que o jogador fosse capaz de acumular os 3 \"power-ups\"). A mecânica da jogabilidade não poderia ser mais simples. Havia um botão para o pulo e outros dois para ataque (no Master System só havia um para ataque) - um para socos e outro para chutes. Ao pegar power-ups, o jogador tornava-se maior e mais musculoso, e em consequência, seus ataques tornavam-se mais fortes. Quando o herói tomava a forma de uma fera, o botão de soco tornava-se o de ataque a longa distância (como projéteis) e o de chute passava a ser o de ataque especial (investidas corpo-a-corpo). Ainda uma outra característica do game era que a tela, ainda que lentamente, avançava sozinha, o que obrigava o jogador a ficar alerta com obstáculos (ficar preso entre um obtáculo e o fim da tela significava perder uma vida). As fases eram curtas (ainda mais se o jogador não deixasse escapar nenhum dos 3 primeiros power-ups) e, basicamente, a dificuldade apresentada também era baixa - os chefes de fase tinham estratégias que, quando aprendidas, tornavam-nos fáceis de serem derrotados. Porém o número de vidas que o jogador acumulava eram o total de chances que tinha para completar o jogo, uma vez que o game não contava com recurso de continues.",
              sega, megadrive, adventure);

            var kof97 = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "The King of Fighters '97",
                "Assets/Item/kof97.jpg",
                "The King of Fighters '97 é um videogame produzido no ano de 1997 pela SNK.",
                //content
                "Passado um ano apos o ataque de Goenitz o torneio é anunciado novamente financiado por patrocinadores. A maioria dos participantes do ano passado ganharam o direito de participar novamente. Dois times novos darão a cara nesse ano. Um deles era um time enviado por Geese Howard e composto por Billy Kane, Ryuji Yamazaki que teria tido uma promessa que se ganhasse o torneio receberia o dobro do prêmio e a agente Blue Mary que estava ali para investigar o Billy Kane e Ryuji Yamazaki secretamente. O outro time New Faces que ainda desconhecido era composto por Yashiro Nanakase, Shermie, e a criança chamada Chris que substituiram o Time dos Esportes Americanos (Aquele do 94, formado por Heavy D, Lucky Glauber e Brian Battler). E assim o torneio prossegue por alguns dias. Nos dias finais, Iori Yagami e Leona Heidern desaparesceram sendo encontrados a algum tempo depois. Porem, eles não eram mais os mesmos. Ralf Jones e Clark Steel , os dois aliados de Leona, prometeram que parariam a agitação de Leona e pediram para Kyo Kusanagi fazer o mesmo com Iori. Kyo e seus amigos conseguiram parar Iori assim como Ralf e Clark, parando Leona. Yashiro dá um passo a frente e diz que eles são três dos 4 reis divinos de Orochi. O quarto era Goenitz. No meio da confusão foi revelado que até mesmo Yamazaki tinha Sangue de Orochi e que também eles estão atras da irmã de kyo para sacrificá-lá e assim libertar orochi. Depois de uma longa batalha o time New Faces foi derrotado, Nesse instante, Chris, começou a flutuar entre os outros dois reis de Orochi. De repente, Yashiro e Shermie se matam bem em frente aos olhos de Chris! Assim Chris se tranforma em um jovem de cabelo branco e torax tatuado e ele diz que toda a humanidade morrerá em suas mãos e que agora terminaria o que começou a 1800 anos atrás. E assim chega a hora de lutar contra Orochi. Com apenas um golpe ele derruba todos os lutadores, assim Kyo foi jogado ao chão começando a perder a consciência. Entre a escuridão, Kyo podia ouvir vozes semelhantes a espíritos. Elas contaram para Kyo que eram os espíritos dos Yagami, antepassados mortos de Iori. Os Yagami pediram a Kyo para acabar com a maldição que lhes foi imposta desde o pacto de sangue com Orochi, à 660 anos atrás. Assim Kyo de levanta lentamente junto a Iori, que teria voltado ao seu estado normal. Iori diz a Kyo \"Parece que nós morreremos juntos, não é Kusanagi? Você não morrerá nas minhas mãos\". Chizuru Kagura chama ambos e lhes fala que ela levará Orochi mais uma vez aos cuidados do selo mas para isso ele deve ser derrotado. Assim Kyo e Iori voltam a luta e com suas ultimas forças conseguem derrotar Orochi. A batalha de 1800 anos atrás se repetia. Kusanagi e Yagami lutando juntos e Kagura cuidando do selo. Ao término da batalha, Orochi se cansou e usou todo seu poder para liberar a \"Revolta do Sangue\" novamente em Iori. Iori começou a ficar insano novamente. Orochi ordenou que Iori atacasse Kyo e Chizuru, matando ambos. Porém, Orochi que foi atacado por Iori! Iori agarra Orochi pelo pescoço e Chizuru pede a Kyo que dê um fim à batalha. Kyo se nega, dizendo que acabaria matando Iori também. Chizuru diz que ele não tem mais tempo. Iori não seguraria Orochi por muito tempo e Iori apenas perdera o poder dado por Orochi. Assim Kyo junta toda sua força e dá um golpe devastador, e Orochi é finalmente derrotado. Assim termina o torneio, sem ser definido o verdadeiro campeão.",
                sony, playStation, playStation2, fight);

            var re4 = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "Resident Evil 4",
                "Assets/Item/re4.jpg",
                "Resident Evil 4 é um jogo eletrônico de survival horror e, diferentemente de seus antecessores, ação e aventura desenvolvido e lançado pela Capcom. Resident Evil 4 é o sexto jogo da série de survival horror Resident Evil. Foi lançado em 11 de Janeiro de 2005, nos Estados Unidos, originalmente como um jogo exclusivo para o console GameCube. Uma versão para PlayStation 2 foi lançada no dia 25 de Outubro de 2005, com adições de jogabilidade. Versões para PC e Wii também foram lançadas, em março e maio de 2007,[1] respectivamente, com uma versão para celular também anunciada no mesmo ano. Resident Evil 4 revolucionou a franquia da Capcom, com jogabilidade mais focada na ação mas sem se esquecer da essência da série, teve grande influência em outros jogos de vários gêneros, sendo considerado por algumas publicações um dos melhores jogos de survival horror.",
                //content
                "O jogo começa mostrando um pequeno resumo sobre os acontecimentos de antigos jogos da série, e uma novidade: é anunciada a falência da Umbrella Corporation; devido as ações de Chris Redfield, Jill Valentine e Albert Wesker, fato relatado em Umbrella Chronicles. O protagonista de RE4 é Leon S. Kennedy, sobrevivente de Raccoon City, que agora trabalha como agente secreto para o governo americano. Leon deve investigar a suspeita de Ashley Graham, a filha do presidente, que foi sequestrada e aparentemente estar numa remota vila na Espanha. Leon é auxiliado por Ingrid Hunnigan, que lhe dá informações sobre o terreno. O principal antagonista é Osmund Saddler, líder do culto Los Illuminados e responsável por infectar os habitantes do vilarejo com a Las Plagas, que planeja fazer o mesmo com o resto do planeta. Para isso, conta com a ajuda de Ramon Salazar, responsável por liberar as Plagas; Bitores Mendez, o prefeito do vilarejo; e Jack Krauser, um ex-companheiro de Leon nos anos de treinamento para o governo, supostamente morto dois anos antes. Ainda são incluídos Luís Sera, um misterioso cientista que trabalhava para Salazar, e se apresenta a Leon como um policial de uma cidade vizinha; Ada Wong, antiga paquera de Leon que, agora trabalha para Wesker, tem como objetivo obter uma amostra do parasita La Plaga; e Albert Wesker, ex-capitão dos S.T.A.R.S. que planeja a reconstrução da Umbrella Corporation. Em 2004, as atividades ilegais da Umbrella dentro de Raccoon City vieram a público. Após uma investigação conduzida pelo governo, vários diretores da empresa são presos. O governo suspende indefinitivamente os negócios da Umbrella, causando sua falência. Leon deve, também, descobrir o que aconteceu ao outro agente enviado anteriormente para resgatá-la. Chegando ao vilarejo rural chamado Pueblo, ele encontra moradores hostis e fervorosamente religiosos, que são capazes de dar suas vidas pelos Los Iluminados. Durante a busca, Leon descobre que os moradores são responsáveis pela morte do agente desaparecido. Leon encontra Ashley e, em seguida, Luís Sera, contratado para investigar o parasita encontrado nas minas de escavação. No decorrer do jogo, diversas anotações de Luís são encontradas, revelando como os parasitas agem e como é possível eliminá-los. Foram feitas escavações em minas e, em uma delas, havia uma substância gasosa que, inevitavelmente, os mineradores inalaram. Pesquisas sobre a substância revelaram que o local onde ela mais se concentrava era sobre ovos petrificados, isso aparentemente. Os animais resultantes destes ovos foram nomeados de La Plaga (A Praga)que esta em RE5 como Cephalo.E Leon Kennedy, tem a missao de resgatar a filha do presidente Ashley que esta presa sobre os Ganados que não sao zumbis e sim humanos infectados. Com o decorrer da história, Leon descobre que não é uma missão apenas para salvar a filha do presidente do EUA, mas sim uma missão para salvar o mundo.",
                sony, playStation2, pc, wii, xbox360, playStation3, action);

            var marioworld = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "Super Mario World",
                "Assets/Item/marioworld.jpg",
                "Super Mario World é um jogo de plataforma desenvolvido e publicado pela Nintendo como um título que acompanhava o console Super Nintendo Entertainment System (SNES). O jogo se tornou um enorme sucesso crítico e comercial, sendo considerado o mais bem-vendido da plataforma, com 20 milhões de cópias vendidas no mundo todo.[3] Como em jogos anteriores da série, o roteiro envolve Mario atravessando terras distintas numa jornada para resgatar a Princesa Peach, que foi capturada por Bowser.",
                //content
                "Mario, Luigi e a Princesa Toadstool foram tirar férias na Ilha dos Dinossauros. Mas, durante as férias, Bowser rapta a amável Princesa! Ele também se apodera da Ilha dos Dinossauros, e aprisiona seus habitantes em ovos mágicos, entregando-os aos seus sete filhos! Mario e Luigi enfrentam desafios por 7 mundos diferentes, até enfim derrotar mais uma vez o terrível Bowser e salvar a Princesa e os habitantes da Ilha dos Dinossauros.\n" +
                "O jogo pode ser jogado com dois jogadores: em uma rodada, o jogador 1 controla Mario e na outra rodada o jogador 2 controla Luigi (na versão para o Game Boy Advance, pode ser escolhido um dos dois personagens sem ficar esperando por turnos).\n"+
                "O objetivo geral consiste em explorar os 9 mundos e encontrar todas as 96 saídas existentes, resgatando a Princesa das garras de Bowser e libertando os sete Yoshis cativos nos ovos. Entretanto, desafios mais amplos podem se tornar suas metas conforme o jogo progride.\n"+
                "Também é relevante encontrar os quatro Palácios do Interruptor ( ! amarelos, azuis, vermelhos e verdes) para tornar a jornada mais fácil, assim também como os mundos secretos como o Caminho da Estrela, que auxilia seu transporte em atalhos estratégicos espalhados por todo o mapa e a Zona Especial, uma coletânea de testes difíceis envolvendo todas as características de jogabilidade trazidas pelo jogo.",
                nintendo, snes, adventure);

            var fableII = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "Fable II",
                "Assets/Item/fableII.jpg",
                "Fable II é um jogo desenvolvido exclusivo para Xbox 360, foi produzido pela Lionhead Studios e lançado pela Microsoft Game Studios. Este jogo é uma seqüência do jogo Fable e Fable: The Lost Chapters. Anunciado em 2006, o jogo foi lançado no outono Norte Americano de 2008. O jogo é uma continuação da história de Albion, 500 anos após o primeiro Fable. As armas de fogo estão presente, e os mapas tem uma aparência mais desenvolvida (grandes castelos e cidades no lugar de Vilarejo). Diferente da primeira versão, nesta edição há a possibilidade de escolher o sexo do personagem.",
                //content
                "Há duas cenas interativas e não-interativo no jogo. Segundo a Lionhead Studios, as cenas não-interativas consumir menos de cinco minutos de tempo de jogo.\n" +
                "Nas cenas totalmente interativas o jogador pode usar suas expressões durante o diálogo ou mesmo fugir da cena, assim que pular, depois o jogador pode retornar ao local cutscene para iniciá-lo novamente. Se o jogador foge de uma cena que contém informações importantes, o personagem vai esperar a volta do jogador.\n"+
                "Companheiro do jogador é um cão que faz amizade com o jogador como uma criança. Este cão segue o jogador quase todo o tempo durante o jogo. O cão pode aprender truques, inimigos de luta e encontrar o tesouro, e liderar o caminho para os objectivos Quest (quando necessário, embora isso seja raro, normalmente o jogador é levado para os objectivos através de um espumante \"migalha de pão\" trilha do ouro). Ela também pode ajudar em situações de combate, atacando inimigos abatidos. O cão não pode ser morto, mas pode tornar-se lesionado e ineficaz, exigindo a cura pelo jogador.\n" +
                "A aparência do seu cão também espelham as escolhas do jogador e muda de cor dependendo alinhamentos do jogador, se o jogador é neutro ele permanecerá cinza, ser bom vai virar o revestimento do cão ao dourado e do mal vai transformá-lo em preto. Não há outros animais no jogo, salvo coelhos neutros e aves, fato comentado por um NPC que percebe a estranheza de carruagens sem cavalos.",
                microsoft, xbox360, rpg, action);

            var golden007 = new GamePediaDataItem(Guid.NewGuid().ToString(),
              "GoldenEye 007",
              "Assets/Item/GoldenEye007box.jpg",
              "Goldeneye 007 é um jogo de tiro em primeira pessoa para Nintendo 64, baseado no filme de James Bond, GoldenEye. Produzido pela Rareware (até então produtora second-party da Nintendo) e distribuído pela Nintendo, fora lançado em 1997 (dois anos após o filme). É considerado um dos maiores jogos de 1ª pessoa da história, com foco especial em seu inovador modo multiplayer.",
                //content
              "O jogo se baseia no filme homônimo, com algumas modificações introduzidas pelos programadores, como missões extras e ambientes inexistentes no filme.\n"+
              "Um dos grandes diferenciais de GoldenEye 007 é o seu sistema nos menus do jogo. Ao escolher a missão, o jogador vê tudo como se fosse um dossiê da MI6 britânica, cada save está contido em uma das quatro pastas disponíveis, e o jogador pode apagá-las ou copiá-las. Quando seleciona uma missão, o jogador pode ler informações sobre ela. As informações são fornecidas por membros da MI6: \"M\", que dá o panorama geral da situação e dos objetivos. \"Q\", que informa sobre os aspectos técnicos do lugar e sobre as bugigangas fornecidas por ele e Moneypenny, que faz um rápido comentário com teor amoroso.\n"+
              "O jogo divide-se em 3 dificuldades: Agent, Secret Agent e 00 Agent. Após terminar com todas as fases na dificuldade 00, é liberada a dificuldade 007, que permite modificar diversas opções no jogo, como inteligência artificial dos inimigos e quantidade de energia do jogador. Depois que uma missão é completada, o jogador recebe informações de sua performance, como o tempo de duração da missão e a porcentagem de precisão dos tiros. O jogador pode liberar uma trapaça especial se terminar as missões em uma certa dificuldade em um limite de tempo, aumentando o fator replay do jogo. As trapaças incluem a possibilidade de ficar invencível, invisível (com essa trapaça ativada, Bond fica invisível apenas às pessoas, câmeras e metralhadoras automáticas ainda podem vê-lo), ter todas as armas e munição infinita.",
              nintendo, n64, action);

            var rocknrollracing = new GamePediaDataItem(Guid.NewGuid().ToString(),
                "Rock & Roll Racing",
                "Assets/Item/rocknrollracing.jpg",
                "Rock N' Roll Racing é um jogo eletrônico de corrida que possui um estilo único para um jogo de 1993. Desenvolvido pela Blizzard Entertainment e publicado pela Interplay, o grande forte do jogo é a trilha sonora no estilo rock n' roll, o que adiciona mais emoção às corridas, ultrapassagens e principalmente a batidas \"que acontecem em excesso no decorrer das fases\". Outro destaque é o estilo batalha que acompanha as corridas com tiros e bombas voando para todos os lados.",
                //content
                "A história se desenvolve como se tudo ocorresse em um campeonato interplanetário com competições em vários planetas diferentes, seis no total, cada um com a atmosfera variada:\n"+
                "1.Planeta Chen VI\n"+
                "2.Planeta Drakonis\n"+
                "3.Planeta Bogmire\n"+
                "4.Planeta New Mojave\n"+
                "5.Planeta Nho\n"+
                "6.Planeta Inferno\n"+
                "O interessante é a parte que permite ao jogador poder equipar ao máximo seu carro com armas, latarias, pneus, entre outros equipamentos. Usa o sistema isométrico e a jogabilidade é boa. Existem três níveis de dificuldade: Rookie, Veteran e Warrior, sendo que no mais fácil (Rookie) é possível terminar o jogo em menos de uma hora.\n" +
                "Há versões para Snes e Genesis. Elas são parecidas, porém há algumas diferenças como trilha sonora e alguns elementos gráficos. A jogabilidade é mais difícil na versão para Genesis.",
            nintendo, race, snes, megadrive);

            this.AllGroups.Add(sony);
            this.AllGroups.Add(sega);
            this.AllGroups.Add(nintendo);
            this.AllGroups.Add(microsoft);
            this.AllGroups.Add(genres);
            
            this.AllGroups.Add(action);
            this.AllGroups.Add(adventure);
            this.AllGroups.Add(rpg);
            this.AllGroups.Add(race);
            this.AllGroups.Add(sports);
            this.AllGroups.Add(fight);

            this.AllGroups.Add(xbox360);
            this.AllGroups.Add(xbox);
            this.AllGroups.Add(pc);

            this.AllGroups.Add(playStation);
            this.AllGroups.Add(playStation2);
            this.AllGroups.Add(playStation3);
            
            this.AllGroups.Add(wii);
            this.AllGroups.Add(snes);
            this.AllGroups.Add(n64);
            
            this.AllGroups.Add(megadrive);
        }

        private static void CreateGenres(out GamePediaDataGroupBase genres, out GamePediaDataGenre action, out GamePediaDataGenre adventure, out GamePediaDataGenre rpg, out GamePediaDataGenre race, out GamePediaDataGenre fight, out GamePediaDataGenre sports)
        {
            genres = new GamePediaDataGroupBase(Guid.NewGuid().ToString(),
                          "Gêneros", "Assets/Producer/genre.png", "Gêneros de jogos de vídeo são usados ​​para categorizar videogames baseados em suas interações jogabilidade ao invés de diferenças visuais ou narrativa.",
                          "Um gênero de jogo de vídeo é definida por um conjunto de desafios de jogo. Eles são classificados independentemente da sua configuração ou jogo de mundo de conteúdo, ao contrário de outras obras de ficção, como filmes ou livros. Por exemplo, um jogo de ação ainda é um jogo de ação, independentemente de ele ocorre em um mundo de fantasia ou espaço sideral. Nos estudos de jogo há uma falta de consenso para alcançar aceitas definições formais para gêneros de jogos, alguns sendo mais observados do que outros. Como qualquer taxonomia típico, um gênero de jogo de vídeo requer certas constantes. A maioria dos jogos de vídeo apresentam obstáculos a superar, assim gêneros de jogos de vídeo pode ser definida, onde os obstáculos são concluídos de uma forma substancialmente semelhantes.");

            action = new GamePediaDataGenre(
                Guid.NewGuid().ToString(),
                "Ação",
                "Assets/Genre/action.png",
                "Um jogo de ação exige que os jogadores de usar reflexos rápidos, precisão e tempo para superar os obstáculos. É talvez o mais básico dos gêneros de jogos, e certamente uma das mais ampla. Jogos de ação tendem a ter jogabilidade com ênfase no combate. Há muitos subgêneros de jogos de ação, tais como jogos de luta e tiro em primeira pessoa");

            adventure = new GamePediaDataGenre(
                Guid.NewGuid().ToString(),
                "Aventura",
                "Assets/Genre/adventure.png",
                "Jogos de Aventura foram alguns dos primeiros jogos criados, começando com o texto Colossal Cave Adventure aventura na década de 1970. Esse jogo foi originalmente chamado simplesmente \"Aventura\", e é o homónimo do gênero. Com o tempo, os gráficos foram introduzidas para o gênero e a interface evoluiu.");

            rpg = new GamePediaDataGenre(
                Guid.NewGuid().ToString(),
                "RPG",
                "Assets/Genre/rpg.png",
                "Jogos role-playing vídeo chamar a sua jogabilidade dos tradicionais RPGs como Dungeons & Dragons. A maioria lançou o jogador no papel de um ou mais \"aventureiros\" que se especializam em conjuntos de habilidades específicas (tais como combate corpo a corpo ou feitiços mágicos), progredindo através de um enredo pré-determinado");

            race = new GamePediaDataGenre(
                Guid.NewGuid().ToString(),
                "Simulador",
                "Assets/Genre/race.png",
                "Videogames de simulação é uma diversa super-categoria de jogos, geralmente destinado a simular de perto os aspectos de uma realidade real ou ficcional.");

            fight = new GamePediaDataGenre(
                Guid.NewGuid().ToString(),
                "Luta",
                "Assets/Genre/fight.png",
                "Jogos de luta enfatiza combate um-contra-um entre dois personagens, um dos quais pode ser controlado por computador. Esses jogos geralmente são jogados usando uma sequência de movimentos controlados pelo joystick para causar dano ao adversário.");

            sports = new GamePediaDataGenre(
                Guid.NewGuid().ToString(),
                "Esporte",
                "Assets/Genre/sports.png",
                "Jogos de esportes emular o jogo dos tradicionais esportes físicos. Alguns enfatizam realmente jogar o esporte, enquanto outros enfatizam a estratégia por trás do esporte (como Championship Manager). Outros satirizar o esporte para o efeito cômico (como rival). Uma das séries mais vendido neste gênero é a série FIFA (série de jogos de vídeo). Este gênero surgiu no início da história dos videogames (por exemplo, Pong) e permanece popular até hoje.");

            genres.Groups.Add(action);
            genres.Groups.Add(adventure);
            genres.Groups.Add(rpg);
            genres.Groups.Add(race);
            genres.Groups.Add(fight);
            genres.Groups.Add(sports);
        }

        private static void CreateNintendoAndConsoles(out GamePediaDataGroupBase nintendo, out GamePediaDataConsole snes, out GamePediaDataConsole wii, out GamePediaDataConsole n64)
        {
            nintendo = new GamePediaDataGroupBase(Guid.NewGuid().ToString(),
                "Nintendo", "Assets/Producer/nintendo.png", "Nintendo Co., Ltd. é uma empresa japonesa de eletrônicos multinacional consumidor localizado em Kyoto, Japão. Fundada em 23 de setembro de 1889 por Fusajiro Yamauchi, produziu cartões artesanais Hanafuda. Em 1963, a empresa já havia tentado vários negócios pequenos nichos, como uma empresa de táxi e um amor hotel.Nintendo é a empresa maior do mundo de jogos em receita.",
                "Nintendo desenvolvido em uma empresa de jogos de vídeo, tornando-se o mais influente na indústria, [carece de fontes?] E empresa mais valiosa do Japão, terceira na lista, com um valor de mercado de mais de EUA $ 85 bilhões. Nintendo of America também é o proprietário da maioria dos Seattle Mariners equipe da Major League Baseball.\n " +
                "O nome Nintendo pode ser grosseiramente traduzido do japonês para o Inglês como \"deixe a sorte para o céu\". A partir de 18 de outubro de 2010, a Nintendo já vendeu mais de 565 milhões de unidades de hardware e 3,4 bilhões de unidades de software."
            );

            snes = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "Super Nintendo",
                    "Assets/Console/sn 480x360.png",
                    "O Super Nintendo Entertainment System (também conhecido como o Super NES, SNES ou Super Nintendo) é um 16-bit console de vídeo game que foi lançado pela Nintendo na América do Norte, Europa, Austrália (Oceania) e América do Sul entre 1990 e 1993.");

            wii = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                "Nintendo Wii",
                "Assets/Console/wii 480x360.png",
                "O Wii é um console de videogame lançado pela Nintendo em 19 de novembro de 2006. Como um console de sétima geração, o Wii compete com o Xbox 360 da Microsoft eo PlayStation 3 da Sony. Nintendo estados que a sua consola tem como alvo um mais amplo demográfica do que a dos outros dois. A partir do primeiro trimestre de 2012, o Wii lidera a geração mais o PlayStation 3 e Xbox 360 em vendas mundiais, e em dezembro de 2009, o console quebrou o recorde de mais vendido do console em um único mês nos Estados Unidos.");

            n64 = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                "Nintendo 64",
                "Assets/Console/n64 480x360.png",
                "O Nintendo 64, muitas vezes referida como N64 (estilizado como NINTENDO ⁶⁴, e anteriormente conhecido como o Ultra Nintendo 64) é o terceiro da Nintendo casa console de videogame para o mercado internacional. Nomeado para a sua unidade de processamento de 64 bits central, que foi lançado em Junho de 1996 no Japão, setembro de 1996 na América do Norte, março de 1997 na Europa e Austrália, setembro de 1997 na França e dezembro de 1997 no Brasil. É console da Nintendo última casa para usar cartuchos ROM para armazenar jogos (Nintendo mudou para um formato baseado em MiniDVD para o sucessor do GameCube); handhelds da linha Game Boy, no entanto, continuou a usar GamePaks");

            nintendo.Groups.Add(snes);
            nintendo.Groups.Add(wii);
            nintendo.Groups.Add(n64);
        }

        private static void CreateMicrosoftAndConsoles(out GamePediaDataGroupBase microsoft, out GamePediaDataConsole xbox360, out GamePediaDataConsole xbox, out GamePediaDataConsole pc)
        {
            microsoft = new GamePediaDataGroupBase(Guid.NewGuid().ToString(),
                           "Microsoft", "Assets/Producer/microsoft.png", "Microsoft é dominante no mercado, tanto no sistema operacional do PC e mercados Office (este último com o Microsoft Office). A empresa também produz uma ampla gama de outros softwares para desktops e servidores, e é ativa em áreas como pesquisa na internet (com Bing), a indústria de videogames (com o Xbox e Xbox 360), o mercado digital de serviços (através de MSN) e telefones celulares (através do Windows Phone oS). Em junho de 2012, a Microsoft anunciou que estaria entrando no mercado fornecedor do PC pela primeira vez, com o lançamento do computador tablet Microsoft Surface.",
                           "A Divisão de Entretenimento e Dispositivos produz o sistema operacional Windows CE para sistemas embarcados e Windows Phone para smartphones. Microsoft inicialmente entrou no mercado móvel através do Windows CE para dispositivos portáteis, eventualmente, evoluir para o Windows Mobile OS e agora, o Windows Phone. Windows CE foi projetado para dispositivos onde o sistema operacional não pode ser directamente visível para o usuário final, em particular, eletrodomésticos e automóveis. A divisão também produz jogos de computador que são executados em PCs com Windows e outros sistemas, incluindo títulos como Age of Empires, Halo e do Microsoft Flight Simulator série, e abriga a Unidade de Negócios Macintosh que produz software Mac OS, incluindo Microsoft Office 2011 para Mac. Entretenimento da Microsoft e projetos Devices Division, comercializa e fabrica produtos eletrônicos de consumo, incluindo o 360 jogo console Xbox, o portátil Zune media player, e na televisão baseado em Internet aparelho de TV MSN. A Microsoft também comercializa o hardware do computador pessoal, incluindo ratos, teclados e controladores de jogos diversos, tais como joysticks e gamepads.");

            xbox360 = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "Xbox 360",
                    "Assets/Console/xb 480x360.png",
                    "O Xbox 360 é o console segundo jogo de vídeo desenvolvido pela e produzido para a Microsoft e sucessor do Xbox. O Xbox 360 concorre com o PlayStation 3 da Sony eo Wii da Nintendo como parte da sétima geração de consoles de videogame. A partir de 19 de abril de 2012, 67,2 milhões de consoles Xbox 360 foram vendidos no mundo.");

            xbox = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "Xbox",
                    "Assets/Console/xb 480x360.png",
                    "O Xbox é um console de videogame fabricado pela Microsoft. Foi lançado em 15 novembro de 2001 na América do Norte, 22 de fevereiro de 2002 no Japão, e 14 de março de 2002 na Austrália e na Europa. Foi a primeira incursão da Microsoft no mercado de consoles de jogos. Como parte da sexta geração de jogos, o Xbox competiu com PlayStation 2 da Sony, o Dreamcast da Sega (que parou as vendas americanas antes do Xbox foi colocado à venda), e GameCube, da Nintendo. O Xbox foi o primeiro console oferecido por uma empresa norte-americana após a Atari Jaguar parou de vendas em 1996.");

            pc = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "PC Games",
                    "Assets/Console/pc 480x360.png",
                    "Um jogo de PC, também conhecido como um jogo de computador, é um jogo de vídeo jogado em um computador pessoal, em vez de uma consola de jogos de vídeo ou máquina de arcade. Jogos de PC evoluíram a partir dos gráficos simples e jogabilidade de títulos adiantados como Spacewar!, Para uma ampla gama de títulos visualmente mais avançadas.");

            microsoft.Groups.Add(xbox360);
            microsoft.Groups.Add(xbox);
            microsoft.Groups.Add(pc);
        }

        private static void CreateSonyAndConsoles(out GamePediaDataGroupBase sony, out GamePediaDataConsole playStation3, out GamePediaDataConsole playStation2, out GamePediaDataConsole playStation)
        {
            sony = new GamePediaDataGroupBase(Guid.NewGuid().ToString(),
                "Sony", "Assets/Producer/sony.png", "Sony Computer Entertainment é mais conhecida por produzir o popular linha de consoles PlayStation. A linha surgiu de uma parceria com a Nintendo que não deu certo. Originalmente, a Nintendo pediu para a Sony para desenvolver um add-on para o seu console que iria jogar Compact Discs. Em 1991 a Sony anunciou o add-on, bem como um console dedicado conhecido como o \"Play Station\". No entanto, um desacordo sobre o licenciamento de software para o console fez com que a parceria se dissolver. Sony continuou o projeto de forma independente.",
                "Lançado em 1994, o PlayStation primeiro ganhou 61% das vendas de consolas globais e quebrou liderança da Nintendo de longa data no mercado. Sony seguiu com o PlayStation 2 em 2000, que foi ainda mais bem sucedido. O console se tornou o mais bem sucedido de todos os tempos, vendendo mais de 150 milhões de unidades a partir de 2011. A Sony lançou o PlayStation 3, um console de alta definição, em 2006. Foi o primeiro console a utilizar o formato Blu-ray, embora o seu processador Cell tornou consideravelmente mais caros do que os concorrentes Xbox 360 e Wii. Logo no início, baixo desempenho de vendas resultou em perdas significativas para a empresa, levando-o a vender o console em uma perda. O PlayStation 3 tem vendido geralmente mais mal do que os concorrentes, embora não por uma margem grande. Ele mais tarde introduziu o PlayStation Move, um acessório que permite aos jogadores controlar jogos de vídeo usando gestos de movimento.\n" +
                "Sony ampliou a marca para o mercado de jogos portáteis em 2005 com o PlayStation Portable (PSP). O console já vendeu razoavelmente, mas tomou um segundo lugar para um portátil rival, o Nintendo DS. Sony desenvolveu o Universal Media Disc (UMD) meio de disco óptico para uso no PlayStation Portable. Logo no início, o formato foi usado para filmes, mas, desde então, perdeu o apoio dos grandes estúdios. A Sony lançou uma versão disco-menos da sua PlayStation Portable, o PSP Go. A empresa passou a lançar seu sistema segundo jogo portátil de vídeo, PlayStation Vita, em 2011 e 2012.\n" +
                "Sony Online Entertainment opera serviços online para PlayStation, bem como vários outros jogos online. Em 2011, hackers invadiram o serviço on-line PlayStation Network, roubar as informações pessoais de 77 milhões de correntistas.");


            playStation3 = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "Playstation 3",
                    "Assets/Console/ps3 480x360.png",
                    "Lançado em 11 de novembro de 2006, o PlayStation 3 é a terceira iteração e atual da série. Ele concorre com o Xbox 360 eo Wii na sétima geração de consoles de videogame. É o primeiro console da série a introduzir o uso de controles de movimento nos jogos através da utilização do Comando sem fios SIXAXIS, juntamente com outras características, tais como Blu-ray Disc (BD) e de alta definição resolução capacidade gráfica. A PlayStation 3 vem em 20 GB, 40 GB, 60 GB, 80 GB, 120 GB, 160 GB, 250 GB e 320 GB, com apenas a 160, e 320, sendo os modelos atuais. Como seus predecessores, um modelo mais fino redesenhada do console foi lançado. Segundo a Sony Computer Entertainment, o PlayStation 3 já vendeu 63,9 milhões de unidades em todo o mundo em 31 de março de 2012.");
            playStation2 = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "Playstation 2",
                    "Assets/Console/ps3 480x360.png",
                    "Lançado em 2000, 15 meses após o Dreamcast e um ano antes de seus outros concorrentes, o Xbox eo GameCube da Nintendo, o PlayStation 2 faz parte da sexta geração de consoles de videogame, e é retro-compatível com jogos de PlayStation mais originais. Como seu antecessor, que recebeu um elegante redesenho, e também foi lançado built-in para o DVR PSX e da Sony BRAVIA HDTV KDL22PX300. É o console mais bem sucedido no mundo, tendo vendido mais de 150 milhões de unidades em 31 de janeiro de 2011. Em 29 de novembro de 2005, o PS2 foi o console de jogo mais rápido para chegar a 100 milhões de unidades vendidas, realizando o feito no prazo de 5 anos e 9 meses de seu lançamento. Essa conquista ocorreu mais rápido do que seu antecessor, o PlayStation, que teve \"9 anos e 6 meses desde o lançamento\" para alcançar a mesma figura.");
            playStation = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "Playstation",
                    "Assets/Console/ps3 480x360.png",
                    "O PlayStation original, lançado em dezembro de 1994 foi o primeiro da série PlayStation onipresente do console e dispositivos portáteis de jogos. Ele incluiu consoles sucessores e atualizações, incluindo o Yaroze Net (PlayStation preto especial com ferramentas e instruções para os jogos do programa PlayStation e aplicações), \"PSone\" (uma versão menor do original) eo PocketStation (a mão que melhora a jogos de PlayStation e também actua como um cartão de memória). Era parte da quinta geração de consoles de videogame concorrentes contra o Sega Saturn eo Nintendo 64. Em 31 de março de 2005, o PlayStation e PSone tinha enviado um total de 102.49 milhões de unidades, tornando-se o console primeiro jogo de vídeo para vender 100 milhões de unidades");

            sony.Groups.Add(playStation3);
            sony.Groups.Add(playStation2);
            sony.Groups.Add(playStation);
        }

        private static void CreateSegaAndConsoles(out GamePediaDataGroupBase sega, out GamePediaDataConsole megadrive)
        {
            sega = new GamePediaDataGroupBase(Guid.NewGuid().ToString(),
                "Sega", "Assets/Producer/sega.png",
                "Sega Corporation, está localizada em Ota, Tóquio, Japão. Divisão européia da Sega, Sega Europe Ltd., está sediada na área Brentford de Londres, no Reino Unido.",
                "Sega Corporation, pronunciado sæ'ɡə (EUA / Canadá / Reino Unido) ou si'ɡə (Austrália / Nova Zelândia), geralmente denominado como SEGA, é uma multinacional desenvolvedora de software de vídeo game e um software de arcada e empresa de desenvolvimento de hardware com sede em Ota, Tóquio, Japão, com escritórios ao redor do mundo. Sega previamente desenvolvido e fabricado sua própria marca de consoles caseiros de videogame 1983-2001, mas uma reestruturação foi anunciada em 31 de janeiro de 2001, que cessou a produção continuou a sua consola doméstica existente, efetivamente sair da empresa com o negócio de consoles domésticos. Embora o desenvolvimento de arcade continuaria inalterado, a reestruturação mudou o foco de origem da empresa de desenvolvimento de software de vídeo game para consoles desenvolvidos por vários outros fabricantes.");


            megadrive = new GamePediaDataConsole(Guid.NewGuid().ToString(),
                    "Mega Drive",
                    "Assets/Console/md 480x360.png",
                    "The Sega Genesis, known as the Mega Drive outside North America, is a fourth-generation video game console developed and produced by Sega. It was originally released in Japan in 1988 as Mega Drive (メガドライブ Mega Doraibu?), then in North America in 1989 as Sega Genesis, and in Europe, Australia and other PAL regions in 1990 as Mega Drive. The reason for the two names is that Sega was unable to secure legal rights to the Mega Drive name in North America. The Sega Genesis is Sega's third console and the successor to the Sega Master System with which it has backward compatibility when the separately sold Power Base Converter is installed");
        
            sega.Groups.Add(megadrive);
        }
    }
}
