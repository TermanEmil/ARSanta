from django.http import JsonResponse
from django.http import HttpResponse
from http import HTTPStatus
import json
import types


def decode(json_response):
    return json.loads(json_response.content.decode('utf-8'))


def apimethod(func):
    def _decorated(request, *args, **kwargs):
        try:
            body = json.loads(request.body)
        except json.decoder.JSONDecodeError:
            body = {}

        result = func(request, *args, **kwargs, **body)
        if issubclass(type(result), HttpResponse):
            return result
        else:
            # Add decode function to this shit
            json_response = JsonResponse(result, safe=False)
            json_response.decode = types.MethodType(decode, json_response)
            return json_response
    return _decorated
