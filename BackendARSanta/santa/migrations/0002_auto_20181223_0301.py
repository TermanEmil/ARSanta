# Generated by Django 2.1.3 on 2018-12-23 03:01

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('santa', '0001_initial'),
    ]

    operations = [
        migrations.AddField(
            model_name='treasures',
            name='bombs',
            field=models.BooleanField(default=False),
        ),
        migrations.AddField(
            model_name='treasures',
            name='oranges',
            field=models.IntegerField(default=0),
        ),
        migrations.AddField(
            model_name='treasures',
            name='reindeers',
            field=models.IntegerField(default=0),
        ),
    ]
