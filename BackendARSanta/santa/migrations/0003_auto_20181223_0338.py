# Generated by Django 2.1.3 on 2018-12-23 03:38

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('santa', '0002_auto_20181223_0301'),
    ]

    operations = [
        migrations.AlterField(
            model_name='treasures',
            name='bombs',
            field=models.IntegerField(default=0),
        ),
    ]
