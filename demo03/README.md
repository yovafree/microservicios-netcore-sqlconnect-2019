# Pass Summit 2019 DEMO for DevOps SQL Server CI/CD

In this demo we have created a project that allows to be packaged in a docker container. The demo has backend (Web API C# .NET Core 3.0) and frontend (Angular 8), with some views that will allow us to view the records of the tables via web.

The project connects to an SQL Server which will be in a container.

# Instructions

To run the demo with the configured containers you can use the Docker Compose file in the root folder. Run the following command:

    docker-compose up --build

The services will be deployed in the following ports.

    http://localhost:90 <- Frontend (Angular 8)
    http://localhost:5000/api <- Backend (.NET Core 3.0)
    -host: localhost -port: 1433 <- Database (SQL Server)

# Follow me

[![N|Solid](http://dbamastery.com/wp-content/uploads/2018/08/if_twitter_circle_color_107170.png)](https://twitter.com/dbamastery) [![N|Solid](http://dbamastery.com/wp-content/uploads/2018/08/if_github_circle_black_107161.png)](https://github.com/dbamaster) [![N|Solid](http://dbamastery.com/wp-content/uploads/2018/08/if_linkedin_circle_color_107178.png)](https://www.linkedin.com/in/croblesdba/) [![N|Solid](http://dbamastery.com/wp-content/uploads/2018/08/if_browser_1055104.png)](http://dbamastery.com/)

  

## License

[MIT](/LICENSE.md)

  

[blog]: <http://dbamastery.com/>