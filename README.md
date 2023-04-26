# Asset Breakdown Register

#### [🇵🇹]
Programa para registar as quebras de ativos em ambiente de produção. Possui diversas seções para serem utilizadas por diferentes pessoas numa empresa, com diferentes funções. Os responsáveis pela produção devem registar os ativos e marcar a severidade da sua quebra para estabelecer uma prioridade de manutenção para a equipa de manutenção. O programa exibe um dashboard simples onde qualquer pessoa pode interpretar o status atual das avarias. É utilizada uma base de dados em SQLite para gravar/ler dados.

#### [🇬🇧󠁧󠁢󠁥󠁮󠁧󠁿]
Program to register the asset breakdowns in a production environment. It has several sections to be used by different people in a company, with different roles. The production responsibles should register the assets and mark their severity in order to establish a maintenance priority for the maintenance crew, a.k.a. request. It display a simple dashboard where anyone can interpreter the current status of the breakdowns. It uses a SQLite database to write/read data.

## Dashboard
![home](https://user-images.githubusercontent.com/83494402/234676075-f8998920-a73c-4f8b-9ed4-a3f58a122532.png)
#### [🇵🇹]
O dashboard permite que qualquer pessoa interprete o status atual das avarias. O user pode selecionar 3 visualizações: os pedidos, os ativos em manutenção e o total (mostra ambos). Os dois gráficos representam a quantidade de avarias nas semanas anteriores e os custos envolvidos com as manutenções.

#### [🇬🇧󠁧󠁢󠁥󠁮󠁧󠁿]
The dashboard allows anyone to interprete the current status of the breakdowns. The user can select 3 views: the requests, the assets under maintenance and the total (shows both). The two graphs represent the amount of breakdowns in the previous weeks and the costs involded with the maintenance.

## Maintenance
![maintenance](https://user-images.githubusercontent.com/83494402/234676116-c61a7dc0-d514-4405-8234-39f06097703f.png)
#### [🇵🇹]
Esta secção deve ser utilizada pelo responsável de manutenção, a tabela superior mostra as avarias que necessitam de tratamento. O utilizador deverá inserir um status (pode ser personalizado na secção de configurações). A tabela descendente é usada para gerir o status das solicitações feitas, o status de cada solicitação pode ser atualizado a qualquer momento e a esta pode ser marcada como concluída. Um custo de manutenção pode ser inserido após o aparecimento de um formulário pop-up.

#### [🇬🇧󠁧󠁢󠁥󠁮󠁧󠁿]
This section should be used by the maintenance responsible, the upper table shows the breakdowns that required treatment, the user should insert a status (can be personalized in the settings section). The downwards table is used to manage the status of the requests made, their status can be updated any time and the request can be marked as concluded. A cost of maintenance can be inserted after a popup form appears.

## Register
![register](https://user-images.githubusercontent.com/83494402/234676131-020ed8c4-ed7b-484b-b312-2da5d5920405.png)
#### [🇵🇹]
Esta seção deve ser utilizada pelos responsáveis da produção, eles podem registar (fazer uma nova solicitação) os ativos que necessitam de manutenção. Deve ser inserido pelo menos uma descrição e um valor de severidade. Opcionalmente, cada solicitação pode ser enquadrada numa categoria e subcategoria, o código do ativo pode ser associado e um texto de observação pode ser inserido.

#### [🇬🇧󠁧󠁢󠁥󠁮󠁧󠁿]
This section should be used by the production responsibles, they can register (make a new request) the assets that require maintenance. It should be inserted at least a description and a severity value. Optionally each request can be fit into a category and subcategory, a asset code can be associated and a observations text can be inserted.

## History
![history](https://user-images.githubusercontent.com/83494402/234676099-a0a732fb-b147-484c-b558-70b9b1f2acbe.png)
#### [🇵🇹]
Esta seção apresenta todos os dados sobre as manutenções concluídas. Pode ser exportado para um ficheiro Excel.

#### [🇬🇧󠁧󠁢󠁥󠁮󠁧󠁿]
This section provides all the data about the concluded maintenance. It can be exported in a excel file.

## Settings
![settings](https://user-images.githubusercontent.com/83494402/234676022-18cb53dd-767a-4d32-8f63-acdbb6ff0c1b.png)
#### [🇵🇹]
Várias configurações podem ser feitas na aplicação. O grau de severidade pode ser alterado. Podem ser adicionados novos status que caracterizam a situação atual da solicitação. As categorias e subcategorias podem ser modificadas. O painel direito está em construção, no momento em que cada ação importante é realizada aparece um pop, este pode ser desactivado. Finalmente, a base de dados pode ser apagada.

#### [🇬🇧󠁧󠁢󠁥󠁮󠁧󠁿]
Several configurations can be made on the app. The severity degree can be changed. New status that characterize the current situitation of the request can be added. Categories and subcategories can be modified. The right panel is under construction, at the moment each time an important action is made a pop appears, this can be disabled. Finally the database can be erased.

## Improvements
#### [🇵🇹]
Geral: melhorar o design das tabelas e botões.
* Seção de manutenção: a atualização de status é feita através de uma drop-down list na coluna status, se for feita uma alteração, a linha deve ser marcada com uma nova cor, assim informa que foi feita uma alteração não registada.
* Seção de configurações: mais configurações devem ser adicionadas para permitir mais personalização. O idioma inglês deve ser implementado. Design diferente para a adição de novas subcategorias, é um processo tedioso.

#### [🇬🇧󠁧󠁢󠁥󠁮󠁧󠁿]
* Overall: improve the design of the tables and buttons.
* Maintenance section: the status update is made using a dropdown list in the status column, if a changed is made the line should be marked with a new color, it awares that a unrecorded change was made.
* Settings section: More settings should be added to allow more personalization. English language should be implemented. Different design for the adition of new subcategories, it's a tidious process.

