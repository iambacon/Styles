# Styles

Welcome to Styles, the pattern library for iambacon websites.

## Table of contents

- [Styles](#styles)
  - [Table of contents](#table-of-contents)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
      - [Clone the repo](#clone-the-repo)
      - [Install npm dependencies](#install-npm-dependencies)
      - [Run Styles](#run-styles)
  - [Usage](#usage)
  - [License](#license)
  - [Acknowledgements](#acknowledgements)

## Getting Started

To get Styles running locally, follow these steps.

### Prerequisites

You'll need [Git](https://help.github.com/articles/set-up-git/) and [Node.js](https://nodejs.org/en/) installed to get this project running.

This project is tested with Node.js 24 and npm 11. If you use [nvm](https://github.com/nvm-sh/nvm), run:

```sh
nvm use
```

### Installation

#### Clone the repo

```sh
git clone https://github.com/iambacon/Styles.git
```

#### Install npm dependencies

```sh
npm install
```

#### Run Styles

This will build, serve and watch for changes. Browse to your local instance at `http://localhost:8080`.

```sh
npm start
```

## Usage

To use Styles in your project you will need to generate the CSS file locally.

```sh
npm run build
```

A minified CSS file will be output to `dist/css/style.css`.

To build the documentation site and run the same verification as CI:

```sh
npm run check
```

## License

Distributed under the [MIT License](/LICENSE.MD).

## Acknowledgements

If you would like to read more about why I created a pattern library and the technology choices I made, please read this post [I've finally created UI documentation for my blog with 11ty!](https://www.iambacon.co.uk/blog/ive-finally-created-docs-for-my-blog-ui-with-11ty)
