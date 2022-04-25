using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
namespace Puzzle
//todo // Store and collect colour
{ public class WireConnector : MonoBehaviour {
        public bool active;
        [HideInInspector] public List<Wire> wires;
        public bool andGate, orGate, nandGate, norGate, xorGate, xnorGate;
        private Wire m_WrongWire;
        private void Start() {
            Invoke(nameof(FixWireList), 0);
        }
        void FixWireList() {
            foreach (var w in wires.Where(w => w.button == null)) {
                m_WrongWire = w;
            } wires.Remove(m_WrongWire);
        }
        private void Update() => ActivityCheck();
        private bool ActivityCheck() {
            active = false;
            if (!andGate && !orGate && !nandGate && !norGate && !xorGate && !xnorGate)
                throw new WarningException("No settings have been selected for this " + name);
            if (wires != null) {
                var activeWires = 0;
                var inActiveWires = 0;
                if (andGate) {
                    foreach (var w in wires) {
                        if (w.active)
                            activeWires++;
                        if (activeWires == wires.Count) active = true;
                    }
                } else if (orGate) {
                    foreach (var w in wires) {
                        if (w.active)
                            active = true;
                    }
                } else if (nandGate)
                {
                    foreach (var w in wires) {
                        if (!w.active)
                            active = true;
                    }
                } else if (norGate) {
                    foreach (var w in wires) {
                        if (!w.active)
                            inActiveWires++;
                        if (inActiveWires == wires.Count) active = true;
                    }
                } else if (xorGate) {
                    foreach (var w in wires) {
                        if (!w.active)
                            inActiveWires++;
                        if (w.active)
                            activeWires++;
                        if (inActiveWires > 0 && inActiveWires < wires.Count && activeWires > 0 && activeWires < wires.Count) active = true;
                    }  
                } else if (xnorGate) {
                    foreach (var w in wires) {
                        if (!w.active)
                            inActiveWires++;
                        if (w.active)
                            activeWires++;
                        if (inActiveWires == wires.Count || activeWires == wires.Count) active = true;
                    }
                } else {
                    foreach (var w in wires) {
                        if (w.active)
                            active = true;
                    }
                }
            }
            return active;
        }
    }
}
