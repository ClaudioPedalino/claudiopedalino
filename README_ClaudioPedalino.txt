#Mentions:
	# To setup db need to run the script (to create some initial data and store procedures), and also de postman collection folder called "dummy data checked" create some 'transactions'
	# I had no internet for 4 days, but fortunatelly im on vacations, so i did this in 2 whole days
	# I google a lot, and also see a lot of TimCorey's channels videos.. i recommend this a lot
	# I never did all this things alone, i work in a factory and usually the architects lead some configurations, i learned so much doing this challenge, even in things i didnt get up, like Automapper, i can not doing this work :( But i refresh some concepts and get new things
	# I do the whole application, and then i write some tests, i know isnt the right way but i have almost null experiencie with test and left from later..

#I use:
_Mediator => pattern with MediaR library to Handle cross api
_EFCore => Create db and manage the main operation
_Dapper => Other queries only to read data
_FluentValidation => validate incoming request
_Swagger => Only ui, i never use the documentation feature, sorry for that
_Serilog => MSQLLib to Log.errors (i didnt enrich text)
_Redis => To log the dapper read queries and save result
_NUnit => Testing (i have almost none experiencie writing tests, sorry.. i need to improve that)
_Postman => the collection with some dummydata to load db and the request i made
_Docker => I didnt understand if u said to dockerize api or run some service with docker.. but i dockerize the api

# Docker Image => https://hub.docker.com/repository/docker/17891789/totvschallengepocapi
# In my work i had no problems (because Windows Enterprise) but im home now and use windows home, so i need to tricky docker with this
	https://itnext.io/install-docker-on-windows-10-home-d8e621997c1d
	
