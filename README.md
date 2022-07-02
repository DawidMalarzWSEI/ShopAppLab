# Temat projektu: Aplikacja do zarządzania sklepami

## Wizualnie
### Okno Logowania
- Pola do wprowadzania danych - przycisk "ZALOGUJ"
- Dane logowania w bazie danych.
- Komunikat o niepoprawnym haśle lub nazwie użytkownika

### Okno sidebar - Funkcje aplikacji
- Zakładka z danymi pracowników
- Zakładka z zadaniami dla każdego z pracowników
- Lista sklepów
- Lista pracowników

#### Zakładka z danymi pracowników
- Tabela z danymi pracownika 
- Wyszukiwanie pracowników według `nazwy sklepu`, `stanowiska`, `id użytkownka`, `imieniu`, `nazwisku`
- Przyciski do `dodawania`, `usuwania`, `edycji` pracownika

#### Zakładka z zadaniami dla każdego z pracowników
- Tabela z taskami
- Wyszukiwanie tasków
- Przycisk do `dodawania`, `usuwania`, `edycji` pracownika

#### Lista sklepów
- Tabela z listą sklepów

#### Lista pracowników
- Tabela z pracownikami + nazwa sklepu

## Technicznie:
### Baza danych
- Tabela Pracownicy
- Tabela Sklepy
- Tabela Zadania
- Tabela Grupy
- Tabela Stanowiska

### ORM + operacje na bazie
- Dodawanie rekordów do bazy
- Usuwanie rekordów
- Pobieranie rekordów jako dane

# Instalacja projektu w wersji deweloperskiej

## Pliki
- Skrypt SQL
- Plik projektowy

## Wymagania
- Visual Studio 2022 z .NET 6.0
- MS SQL Server

## Instalacja

Do uruchomienia aplikacji nasz serwer SQL musi być widoczny pod adresem "**.**". Następnie urachamiamy dołączony skrypt. Po dodaniu bazy danych przechodzimy do pliku projektowego a następnie `ShopApp\ShopApp\bin\Debug\net6.0-windows\Images` i tworzymy folder `Images`. Po wkonaniu wszystkich poprzednich operacji uruchamiamy ShopAppLab.sln i aplikajca jest gotowa do użycia.
