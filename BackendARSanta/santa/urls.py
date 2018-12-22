from django.urls import path
from . import views

urlpatterns = [
    path('', views.index, name='index'),
    path('get_treasures', views.get_treasures),
    path('get_treasure/<int:pk>', views.get_treasure),
    path('get_treasures_on_model/<model_image_name>', views.get_treasures_on_model),

    path('add_treasure', views.add_treasure),
]