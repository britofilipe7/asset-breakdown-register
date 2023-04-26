# Asset Breakdown Register

Program to register the asset breakdowns in a production environment. It has several sections to be used by different people in a company, with different roles. The production responsibles should register the assets and mark their severity in order to establish a maintenance priority for the maintenance crew, a.k.a. request. It display a simple dashboard where anyone can interpreter the current status of the breakdowns. It uses a SQLite database to write/read data.

## Dashboard
![home](https://user-images.githubusercontent.com/83494402/234676075-f8998920-a73c-4f8b-9ed4-a3f58a122532.png)
The dashboard allows anyone to interprete the current status of the breakdowns. The user can select 3 views: the requests, the assets under maintenance and the total (show both). The two graphs represent the amount of breakdowns in the previous weeks and the costs involded with the maintenance.

## Maintenance
![maintenance](https://user-images.githubusercontent.com/83494402/234676116-c61a7dc0-d514-4405-8234-39f06097703f.png)
This section should be used by the maintenance responsible, the upper table shows the breakdowns that required treatment, the user should insert a status (can be personalized in the settings section). The downwards table is used to manage the status of the requests made, their status can be updated any time and the request can be marked as concluded, a cost of maintenance can be inserted after a popup form appears.

## Register
![register](https://user-images.githubusercontent.com/83494402/234676131-020ed8c4-ed7b-484b-b312-2da5d5920405.png)
This section should be used by the production responsibles, they can register (make a new request) the assets that require maintenance. It should be inserted at least a description and a severity value. Optionally each request can be fit into a category and subcategory, a asset code can be associated and a observations text can be inserted.

## History
![history](https://user-images.githubusercontent.com/83494402/234676099-a0a732fb-b147-484c-b558-70b9b1f2acbe.png)
This section provides all the data about the concluded maintenance. It can be exported in a excel file.

## Settings
![settings](https://user-images.githubusercontent.com/83494402/234676022-18cb53dd-767a-4d32-8f63-acdbb6ff0c1b.png)
Several configurations can be made on the app. The severity degree can be changed. New status that characterize the current situitation of the request can be added. Categories and subcategories can be modified. The right panel is under construction, at the moment each time an important action is made a pop appears, this can be disabled. Finally the database can be erased.

## Improvements
* Overall: improve the design of the tables and buttons
* Maintenance section: the status update is made using a dropdown list in the status column, if a changed is made the line should be marked with a new color, it awares that a unrecorded change was made.
* Settings section: More settings should be added to allow more personalization. English language should be introduced. Different design for the adition of new subcategories, it's a tidious process.

