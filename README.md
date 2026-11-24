# 🏰 **Quimera: Cerco dos Mortos**

> *Os mortos marcham sobre Quimera. Ergue as tuas defesas, proteja o castelo e salve a coroa. **Contamos com a vossa coragem!***

**Quimera: Cerco dos Mortos** é um jogo de **Tower Defense** de temática medieval e *zumbi*, com visão isométrica, focado em **planejamento tático e posicionamento**. Hordas de mortos-vivos saem da caverna e atravessam o percurso até o castelo que deves proteger. Para defender o reino, deves construir torres ao longo do caminho, eliminar seus inimigos, ganha ouro e reforçar as tuas defesas a cada nova onda.

---

## 📑 Índice

- [História](#-história)
- [Download](#%EF%B8%8F-download)
- [Como jogar](#-como-jogar)
- [Funcionalidades](#-funcionalidades)
- [Torres de defesa](#-torres-de-defesa)
- [Inimigos](#-inimigos)
- [Mecânicas do jogador](#-mecânicas-do-jogador)
- [Vídeo demonstrativo](#%EF%B8%8F-vídeo-demonstrativo)
- [Progressão de dificuldade](#-progressão-de-dificuldade)
- [Tecnologias](#%EF%B8%8F-tecnologias)
- [Como executar o projeto](#-como-executar-o-projeto)
- [Roadmap](#%EF%B8%8F-roadmap)
- [Integrantes](#-integrantes)

---

## 📜 História

Nos confins do mundo conhecido, a outrora modesta colônia de **Quimera**, a joia mais próspera e distante de **Novigrad**, vivia a sua verdadeira Era de Ouro. Sob a liderança afável da **Princesa Penélope**, o reino florescia. Os campos transbordavam com colheitas fartas e as minas vertiam riquezas que garantiam uma vida pacífica e abundante para todos os cidadãos.

A ambição, no entanto, é uma picareta cega. Durante uma expedição aos limites do território, um grupo de mineradores e aventureiros veteranos deparou-se com uma fenda oculta. O que parecia ser apenas mais uma caverna revelou-se uma imensa e antiga cripta. As paredes daquele abismo pulsavam com minérios luminescentes e exóticos, de um valor incalculável, jamais vistos nas terras de Novigrad. Cegados pelo fascínio da riqueza absoluta, os exploradores ignoraram o bom senso e os limites da segurança, adentrando as profundezas escuras sem o preparo adequado.

O preço dessa ganância foi a ruína. Ao escavarem o coração da cripta, os aventureiros quebraram o silêncio de séculos e libertaram uma maldição esquecida. Os antigos soldados, nobres e camponeses sepultados naquele lugar ergueram-se dos seus túmulos, e os mortos voltaram a caminhar. Guiados por uma fome insaciável, os zumbis marcharam em massa para a superfície. O que começou como uma busca por glória e ouro transformou-se numa procissão de destruição, e os gemidos dos mortos agora ecoam pelo vale.

O destino de Quimera está nas tuas mãos. A horda de mortos-vivos avança implacavelmente em direção aos portões do reino. A tua missão é erguer as defesas, posicionar torres e armadilhas ao longo do caminho e proteger o castelo a todo o custo. Não se trata apenas de defender minérios ou muralhas, mas de garantir que o brilho e a esperança do povo de Penélope não sejam engolidos pela escuridão.

---

## ⬇️ Download

[**Click aqui para baixar a sua versão do jogo**](https://github.com/OctavioFPB/war-for-the-crown-TD/releases/latest)

---

## 🎮 Como jogar

**Objetivo:** impedir que os zumbis cheguem ao castelo, derrotando cada onda com as torres que construíres.

1. **Escolhe uma torre** nos botões do canto inferior direito. Cada torre tem um preço em ouro; só é possível selecioná-la se tiveres moedas suficientes.
2. **Posiciona a torre** movendo o mouse sobre o mapa. Uma pré-visualização (*fantasma*) mostra a torre e a **área de alcance** dela. Verde indica que é possível construir ali; vermelho indica que o local é inválido ou está ocupado.
3. **Clica para construir.** O valor é descontado do teu ouro.
4. **Inicia a onda** no botão **GO**. Os zumbis saem da caverna e seguem o caminho até o castelo.
5. **Ganha ouro** ao derrotar os inimigos e usa-o para construir mais torres antes da próxima onda.

| Ação | Controle |
|---|---|
| Selecionar / desselecionar torre | Clique no botão da torre |
| Construir torre | Clique esquerdo do mouse no mapa |
| Iniciar a próxima onda | Botão **GO** |

---
## ▶️ Vídeo demonstrativo

[**Acesso ao vídeo**](https://github.com/user-attachments/assets/09516eeb-fc85-432d-809c-7b704483caf0)

---

## ✨ Funcionalidades

| Funcionalidade | Estado |
|---|---|
| Construção de torres em áreas específicas do mapa, com pré-visualização e indicador de alcance | ✅ Implementado |
| Três tipos de torre selecionáveis, com preço em ouro | ✅ Implementado |
| Sistema de ouro (ganho ao abater inimigos, gasto ao construir) | ✅ Implementado |
| Sistema de ondas com botão **GO** e três categorias de inimigo | ✅ Implementado |
| Torres que escolhem o alvo mais avançado no caminho e disparam projéteis | ✅ Implementado |
| Mapa isométrico com caminhos que se dividem e se unem | ✅ Implementado |
| Pontos de vida do castelo e condição de derrota | ✅ Implementado |
| Menu inicial | ✅ Implementado |
| Pontuação | ✅ Implementado |
| Ranking | ✅ Implementado |

---

## 🗼 Torres de defesa

Cada torre tem uma função tática diferente: alcance, dano e efeito sobre os inimigos.

| Torre | Papel | Alcance | Dano | Preço |
|---|---|---|---|---|
| **Torre dos Arqueiros** | Atira de longe, com cadência alta | Longo | Baixo | 60 |
| **Torre do Canhão** | Defesa pesada para curtas distâncias | Curto | Alto | 50 |
| **Torre Arcana** | Controle da horda com magia de gelo | Médio | Baixo | 70 |

> Os valores são os atuais e podem mudar com o balanceamento do jogo.

Cada torre dispara o seu próprio projétil: a **flecha**, a **bala de canhão** e o **estilhaço de gelo**.

---

## 💀 Inimigos

Há três tipos de inimigos, cada um com sua característica única.

| Inimigo | Perfil | Característica |
|---|---|---|
| **Cavaleiro** | Básico | Equilibrado em velocidade e resistência |
| **Camponês** | Rápido | Veloz, porém frágil |
| **Carrasco** | Lento | Lento, porém muito resistente |

Os inimigos seguem o caminho do mapa e a sua direção de movimento é refletida na imagem que o jogador vê.

---

## 🌊 Progressão de dificuldade

Conforme as hordas são derrotadas e a fase avança, a dificuldade aumenta de forma progressiva: cada onda traz mais inimigos e de tipos diferentes, desafiando as habilidades do jogador, que deverá se adaptar às novas hordas que ameaçarão o seu progresso.

Cada onda define a sua **duração** e a **quantidade de inimigos** de cada categoria (básicos, rápidos e fortes), e é iniciada pelo próprio jogador.

---

## 🛠️ Tecnologias

| | |
|---|---|
| **Motor de jogo** | Unity 6 (`6000.6.7f1`) |
| **Linguagem** | C# |
| **Interface** | Unity UI + TextMeshPro |
| **Mapa** | Tilemaps em grid isométrico |
| **Arte** | 2D isométrica (Software: LibreSprite) |

---

## 👥 Integrantes

| Nome | Matrícula
|---|---|
| Artur Mesquita Jeager | 188354
| João Pedro de Oliveira de Carvalho | 188402
| Octávio Francisco Petry Bortoluzzi | 188380

---




