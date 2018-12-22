from django.db import models


class Treasures(models.Model):
	author = models.CharField(max_length=32)
	message = models.CharField(max_length=200)
	creation_date = models.DateTimeField('Date created')
