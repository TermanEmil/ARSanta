# AR Santa

A treasure hunting application using Augmented Reality. This is a Hackathon project implemented in 24h.

[![youtube video](https://img.youtube.com/vi/iBIhm-0luh0/0.jpg)](https://www.youtube.com/watch?v=iBIhm-0luh0)

## Tools
  * <img src="./imgs/vuforia.jpg" width="48">
  Augmented Reality API
  <br/>

  * <img src="./imgs/unity_logo.jpg" width="48"> Game Engine
  <br/>

  * <img src="./imgs/django_logo.png" width="48"> Backend

## Description
The users can interact with a set of marks. A mark is a visual reference for the application to remember a place. The users can leave multiple gifts in one place.

A gift contains a message and 3 items:
* Oranges
* Reindeers
* Bombs (max 1)

The users can place these items in the gift for others. The oranges and reindeers were planned to be used as a currency. If a gift contains a bomb, the user who opens the gift, will have to play a mini game in which he must click very fast to remove the bomb. If the bomb explodes, the user drops some of his items.

After the gift is opened, the user has the possibility to watch a video ad to double the items and receive extra bombs.

## Technical stuff
The server is run and it waits for requests. The Unity app communicates with the server to get and send data.
