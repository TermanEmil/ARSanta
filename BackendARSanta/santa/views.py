from django.shortcuts import render
from django.http import HttpResponse, JsonResponse
from django.core import serializers
from django.views.decorators.csrf import csrf_exempt
import json
from datetime import datetime
from django.core import serializers

from shared.json_api import apimethod
from .models import Treasures


def to_json(data):
    serialized = serializers.serialize('json', data)
    return json.loads(serialized)


def index(request):
    return HttpResponse("Hello, world. You're at the polls index.")


@apimethod
def get_treasures(request):
    return to_json(Treasures.objects.all())


@apimethod
def get_treasure(request, pk):
    treasure = Treasures.objects.get(pk=pk)
    return to_json([treasure])


@apimethod
def get_treasures_on_model(request, model_image_name):
    treasures = list(Treasures.objects.filter(model_image_name=model_image_name))
    results = []
    for treasure in treasures:
        result = {
            'id': treasure.pk,
            'author': treasure.author,
            'msg': treasure.message,
            'modelName': treasure.model_image_name
        }
        results.append(result)
    return {'treasures': results}


@csrf_exempt
@apimethod
def add_treasure(request, author, msg, model_image_name):
    treasure = Treasures(
        author=author,
        message=msg,
        creation_date=datetime.now(),
        model_image_name=model_image_name)

    treasure.save()
    return to_json([treasure])
