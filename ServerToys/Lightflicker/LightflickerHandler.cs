using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Server;
using Interactables.Interobjects;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Features.Wrappers;
using MEC;
using UnityEngine;
using Cassie = Exiled.API.Features.Cassie;
using Map = Exiled.API.Features.Map;
using RoundEndedEventArgs = LabApi.Events.Arguments.ServerEvents.RoundEndedEventArgs;

namespace ServerToys.Lightflicker;

public class LightflickerHandler
{
    private bool _isCassieDeactivated = false;

    public void OnRoundStarted() => Timing.RunCoroutine(FlickerHandler(), "CassieLightFlicker");

    public void OnRoundEnded(RoundEndedEventArgs ev)
    {
        Timing.KillCoroutines("CassieLightFlicker");
        
        Map.TurnOffAllLights(0);
        _isCassieDeactivated = false;

        Cassie.MessageTranslated(
            "Cassie systems activated . all generators activated .G5 .G2",
            "Системы CASSIE включены.", false, false);
    }

    public void OnCassieAnnouncing(CassieAnnouncingEventArgs ev)
    {
        if (_isCassieDeactivated)
            ev.IsAllowed = false;
    }

    public IEnumerator<float> FlickerHandler()
    {
        while (true)
        {
            var time = Random.Range(900, 1501);
            
            yield return Timing.WaitForSeconds(time);
            
            Cassie.MessageTranslated(
                "Attention all personnel . cassie systems overheat . . overcharge in . tminus . 60 seconds",
                "Внимание всему персоналу! Системы CASSIE перегреты. Перезагрузка начнёться через 60 секунд.", false, false);
            
            yield return Timing.WaitForSeconds(45f);
            
            Cassie.MessageTranslated(
                "30 seconds",
                "30 секунд ...", false, false);
            
            yield return Timing.WaitForSeconds(20f);
            
            Cassie.MessageTranslated(
                "10 . 9 . 8 . 7 . 6 . 5 . 4 . 3 . 2 . 1 .",
                "10 ... 9 ... 8 ...", false, false);
            
            yield return Timing.WaitForSeconds(14);
            
            Cassie.MessageTranslated(
                "Cassie systems deactivated . all generators deactivated .G5 .G2",
                "Системы CASSIE отключены.", false, false);

            Map.TurnOffAllLights(60);
            _isCassieDeactivated = true;
            
            yield return Timing.WaitForSeconds(60);
            
            _isCassieDeactivated = false;
            
            Cassie.MessageTranslated(
                "Cassie systems activated . all generators activated .G5 .G2",
                "Системы CASSIE включены.", false, false);
        }
    }
}