Welcome
=======

This project has been prepared as a technical test request for Dephion.

The bird's eye view of the project is shown in the image below.

Architecture of the system
^^^^^^^^^^^^^^^^^^^^^^^^^^
.. image:: https://user-images.githubusercontent.com/10263337/97175889-3ddf0200-17a5-11eb-9911-c6acf3b7dd46.png



All requests from outside come to the gateway service. After the necessary operations on the gateway (routing, authentication, etc.), the request is forwarded to the relevant sub-service.

Architecturel parts of the system:
^^^^^^^^^^^^^^^^^^

    1. Service discovery: `Consul <https://www.consul.io>`_
    2. Gateway `Ocelot <https://github.com/ThreeMammals/Ocelot>`_
    3. Message broker: `RabbitMq <https://www.rabbitmq.com/>`_
    4. Distributed cache: `Redis <https://redis.io/>`_
    5. Containerization: `Docker <https://www.docker.com/>`_
    6. Development environment management: `Docker compose <https://docs.docker.com/compose/>`_


Solution structure
^^^^^^^^^^^^^^^^^^

There are two class libraries under the shared folder. 

 1. Reservations.Common: A common class library that each service uses to build its own structure
 2. Reservations.Services.Contracts: A shared class library containing message objects shared by services.
 3. Unit test projects placed under the tests folder.



.. toctree::
   :maxdepth: 2
   :hidden:
   :caption: Introduction

   introduction/testing