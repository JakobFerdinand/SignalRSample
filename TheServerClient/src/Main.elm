module Main exposing (..)

import Browser
import Element exposing (..)
import Html exposing (Html)



-- MAIN


main : Program Int Model Msg
main =
    Browser.document
        { init = init
        , view = view
        , update = update
        , subscriptions = subscriptions
        }


type Msg
    = DoNothing


type alias Model =
    Int



-- INIT


init : Int -> ( Model, Cmd Msg )
init flags =
    ( flags, Cmd.none )



-- UPDATE


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case msg of
        DoNothing ->
            ( model, Cmd.none )



-- VIEW


view : Model -> Browser.Document Msg
view model =
    { title = "The Server Client"
    , body =
        [ layout
            [ width fill
            , height fill
            ]
          <|
            column
                [ centerX
                , centerY
                , spacing 20
                ]
                [ text "Hello World"
                , text (String.fromInt model)
                ]
        ]
    }


subscriptions : Model -> Sub Msg
subscriptions model =
    Sub.none
