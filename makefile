# Makefile

# Vars
SERVER_OUTPUT_DIR := ./build/server
CLIENT_OUTPUT_DIR := ./build/client

SERVER_SOURCES := $(wildcard PrimeNumberService.Server/*)
CLIENT_SOURCES := $(wildcard PrimeNumberService.Client/*)

SERVER_TARGET := $(SERVER_OUTPUT_DIR/PrimeNumberService.Server.exe)
CLIENT_TARGET := $(CLIENT_OUTPUT_DIR/PrimeNumberService.Client.exe)

all: build publish clean

build:
	dotnet build "./PrimeNumberService.Server" -c Release
	dotnet build "./PrimeNumberService.Client" -c Release

client:
	dotnet publish "./PrimeNumberService.Server" -c Release -o $(SERVER_OUTPUT_DIR)
	dotnet publish "./PrimeNumberService.Client" -c Release -o $(CLIENT_OUTPUT_DIR)

clean:
	rmdir /s "./build"

# Phony targets
.PHONY: server client clean