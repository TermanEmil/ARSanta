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


@csrf_exempt
@apimethod
def add_treasure(request, author, msg):
    treasure = Treasures(author=author, message=msg, creation_date=datetime.now())
    treasure.save()
    return to_json([treasure])
