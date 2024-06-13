# Scenario

You are the developer of a small application to track purchases history for cloud compute resources. You will be asked to implement a simple api, a web interface for displaying purchase information, and provide a set of analysis on the data.A repository with demo data has been provided in `PurchaseRepository`. This repository will mock the data layer and defines a domain model.

## API

Using .NET create a simple API controller with two endpoints, one to retrieve all purchases and one to retrieve a single purchase via it’s id.

The all-purchases endpoint should return, id, name, date, and total cost of purchase and support http response codes of 200 OK.

The single purchase endpoint should return name, date, cost, quantity, unit price, description, and support response codes of 200 OK, 404 NOT FOUND.

## Web Interface

Create a web interface using a frontend framework using React js, call the api and display on screen a table / data grid for all purchases, on click of a row, provide additional detail of the purchase to the left-hand side of the grid. A wireframe as been provided in Appendix II.

## Web Interface

Create a new api endpoint to provide high level summary statistics for the data provided. Provide the following datapoints:

Time series of spend per month.
Most expensive month.
Month with the most units bought.
The product name related to the most expensive purchase.
The product name with the most units bought.

## For the backend Api developed in .Net core WebApi (Backend/CloudComputeResourceTracker)

To run the development

### `dotnet build`

Builds the .Net WebApi app in the development mode.\

### `dotnet run`

Runs the .Net WebApi app in the development mode.\
Open [http://localhost:5000](http://localhost:5000) to view it in your browser.

## For the Front developed in React, you will see (Frontend/CloudComputeResourceTracker-ui)

To run the development

### `npm install`

download all packages in package json file.\

### `npm start`

Runs the app in the development mode.\
Open http://localhost:3000 to view it in your browser.\
Open [http://localhost:3000](http://localhost:3000) to view it in your browser.
