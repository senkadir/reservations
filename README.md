# Hello world

This project has been prepared as a technical test request for Dephion.

The architecture of this project is designed as a microservice. The project needs many side applications (RabbitMq, Consul, Redis etc.). It is very difficult to install and configure these applications on the local computer, so the project has been developed in 100% compatibility with Docker and the docker-compose tool is used for infrastructure management. For this reason, Docker Desktop is expected to be installed on the local computer.

# Running

After pulling the project, go to the main directory where docker-compose.yml file is, by command line, give the command "[docker-compose up -d](https://docs.docker.com/compose/reference/up/)".

Whola!

After a while the services on the console line should be ready. It should have a similar result as the picture below

![image](https://user-images.githubusercontent.com/10263337/97156960-7fad7f80-1788-11eb-8f2e-388cf92d4661.png)

# More

If everything is successful up to this step, please visit the documenation website that explains how to test the project.

[Go to the documenatation web site.](https://reservations.readthedocs.io/en/latest/index.html#)

See you there.
