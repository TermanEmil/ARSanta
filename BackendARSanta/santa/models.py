from django.db import models


class Treasures(models.Model):
	author = models.CharField(max_length=32)
	message = models.CharField(max_length=200)
	creation_date = models.DateTimeField('Date created')
	model_image_name = models.CharField(max_length=32, default=None, null=True)
	oranges = models.IntegerField(default=0)
	reindeers = models.IntegerField(default=0)
	bombs = models.IntegerField(default=0)
