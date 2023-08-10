# BS STORE APP
We are developing a book store web application called BSSTOREAPP with a layered architecture.

## Was Build With
[![C#](https://img.shields.io/badge/Csharp-563D7C?style=for-the-badge&logo=Csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/ASP.NET-0090d6?style=for-the-badge&logo=.net&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=PostgreSQL&logoColor=white)](https://www.postgresql.org/)
[![Entity-Framework](https://img.shields.io/badge/EntityFramework-512bd4?style=for-the-badge&logo=nuget&logoColor=white)](https://docs.microsoft.com/tr-tr/ef/)    

## Layers
<details>
    <summary>Toggle Content</summary>

### Entities
<a href="https://github.com/ayhan-karaman/BsStoreApp/tree/master/Entities">Entities Layer</a> It is the layer where the models folder is located, in the model folder we have our asset classes and design them.

### Repositories
<a href="https://github.com/ayhan-karaman/BsStoreApp/tree/master/Repositories">Repositories Layer</a> It contains classes that contain interfaces that enable communication with the database and their concrete structures.

### Services
<a href="https://github.com/ayhan-karaman/BsStoreApp/tree/master/Services">Services Layer</a> It contains interfaces for reading and writing our data according to certain rules, and classes that handle concrete structures of these interfaces.

### Presentation
<a href="https://github.com/ayhan-karaman/BsStoreApp/tree/master/Presentation">Presentation Layer</a> 
        It is the layer that hosts controller classes that handle http requests.

###  WebApiUI
<a href="https://github.com/ayhan-karaman/BsStoreApp/tree/master/WebApiUI">WebApiUI Layer</a> It is the layer where we have opened the presentation layer to the internet.

</details>

## Entities -> Models
 <details> 
   <summary> Toggle Content </summary>
    
    ### Book

    | Name  | Data Type    | Allow Nulls | Default |
    | :---- | :----------- | :---------- | :------ |
    | Id    | int          | False       |         |
    | Title | text         | False       |         |
    | Price | numeric      | False       |         |
    

 </details>



 # Acknowledgements
  ## Zafer CÖMERT

<details> 
        <summary> Zafer Cömert Social Media Show </summary>

[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://www.linkedin.com/in/zafer-cömert-51000367)
[![Linkedin](https://img.shields.io/badge/LinkedIn-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/zafer-cömert-51000367)
[![YouTube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://www.youtube.com/@virtual.campus)
[![WebSite](https://img.shields.io/badge/Zafer_CÖMERT-0078D7?style=for-the-badge&logo=microsoftedge&logoColor=white)](http://www.zafercomert.com/)

</details> 
   